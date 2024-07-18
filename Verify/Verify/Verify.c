#define _CRT_SECURE_NO_WARNINGS

#include "falcon/falcon.h"
#include "falcon/config.h"
#include<stdio.h>
#include "falcon/inner.h"
#include <string.h>

static void*
xmalloc(size_t len)
{
    void* buf;

    if (len == 0) {
        return NULL;
    }
    buf = malloc(len);
    if (buf == NULL) {
        fprintf(stderr, "memory allocation error\n");
        exit(EXIT_FAILURE);
    }
    return buf;
}

static void
xfree(void* buf)
{
    if (buf != NULL) {
        free(buf);
    }
}
static const char base64_chars[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

char encode_base64_char(uint8_t b) {
    return base64_chars[b & 0x3F];
}

char* bytes_to_base64(const uint8_t* bytes, size_t len) {
    size_t encoded_len = ((len + 2) / 3) * 4; // 4 characters for every 3 bytes
    char* base64_str = (char*)malloc(encoded_len + 1); // +1 for null terminator
    if (base64_str == NULL) {
        // Memory allocation failed
        return NULL;
    }

    size_t i, j;
    for (i = 0, j = 0; i < len - 2; i += 3, j += 4) {
        base64_str[j] = encode_base64_char(bytes[i] >> 2);
        base64_str[j + 1] = encode_base64_char(((bytes[i] & 0x03) << 4) | (bytes[i + 1] >> 4));
        base64_str[j + 2] = encode_base64_char(((bytes[i + 1] & 0x0F) << 2) | (bytes[i + 2] >> 6));
        base64_str[j + 3] = encode_base64_char(bytes[i + 2] & 0x3F);
    }

    switch (len % 3) {
    case 1:
        base64_str[j] = encode_base64_char(bytes[i] >> 2);
        base64_str[j + 1] = encode_base64_char((bytes[i] & 0x03) << 4);
        base64_str[j + 2] = '=';
        base64_str[j + 3] = '=';
        break;
    case 2:
        base64_str[j] = encode_base64_char(bytes[i] >> 2);
        base64_str[j + 1] = encode_base64_char(((bytes[i] & 0x03) << 4) | (bytes[i + 1] >> 4));
        base64_str[j + 2] = encode_base64_char((bytes[i + 1] & 0x0F) << 2);
        base64_str[j + 3] = '=';
        break;
    }

    base64_str[encoded_len] = '\0'; // Null-terminate the string
    return base64_str;
}
uint8_t decode_base64_char(char c) {
    if (c >= 'A' && c <= 'Z') return c - 'A';
    if (c >= 'a' && c <= 'z') return c - 'a' + 26;
    if (c >= '0' && c <= '9') return c - '0' + 52;
    if (c == '+') return 62;
    if (c == '/') return 63;
    return 0; // Invalid character
}

char* base64_to_bytes(const char* base64_str) {
    size_t input_len = strlen(base64_str);
    if (input_len % 4 != 0) {
        // Invalid Base64 string length
        return NULL;
    }

    size_t len = (input_len / 4) * 3;
    if (base64_str[input_len - 1] == '=') len--;
    if (base64_str[input_len - 2] == '=') len--;

    char* bytes = (char*)malloc(len + 1); // +1 for null terminator
    if (bytes == NULL) {
        // Memory allocation failed
        return NULL;
    }

    for (size_t i = 0, j = 0; i < input_len; i += 4, j += 3) {
        uint32_t group = (decode_base64_char(base64_str[i]) << 18) |
            (decode_base64_char(base64_str[i + 1]) << 12) |
            (decode_base64_char(base64_str[i + 2]) << 6) |
            (decode_base64_char(base64_str[i + 3]));

        bytes[j] = (group >> 16) & 0xFF;
        if (i + 2 < input_len && base64_str[i + 2] != '=') {
            bytes[j + 1] = (group >> 8) & 0xFF;
        }
        if (i + 3 < input_len && base64_str[i + 3] != '=') {
            bytes[j + 2] = group & 0xFF;
        }
    }

    bytes[len] = '\0'; // Null-terminate the string
    return bytes;
}
char* readFromFile(const char* filename) {
    FILE* file;
    char* content;
    long file_size;
    fopen_s(&file, filename, "r");
    if (file == NULL) {
        printf("Không thể mở file.\n");
        return NULL;
    }
    fseek(file, 0, SEEK_END);
    file_size = ftell(file);
    rewind(file);
    content = (char*)malloc(file_size + 1);
    if (content == NULL) {
        fclose(file);
        return NULL;
    }
    fread(content, 1, file_size, file);
    content[file_size] = '\0';
    fclose(file);
    return content;
}
char* read_pem_key(const char* filename) {
    FILE* fp = fopen(filename, "rb");
    if (fp == NULL) {
        return NULL;
    }

    fseek(fp, 0, SEEK_END);
    long file_size = ftell(fp);
    rewind(fp);

    char* data = malloc(file_size + 1);
    if (data == NULL) {
        fclose(fp);
        return NULL;
    }

    fread(data, 1, file_size, fp);
    data[file_size] = '\0';

    fclose(fp);

    // Tìm vị trí bắt đầu của phần base64
    int start = strstr(data, "-----BEGIN PUBLIC KEY-----") - data;
    start += (int)strlen("-----BEGIN PUBLIC KEY-----") + 1;
    // Tìm vị trí kết thúc của phần base64
    int end = strstr(data, "-----END PUBLIC KEY-----") - data;

    // Tách phần base64 ra khỏi file
    char* base64_data = malloc(end - start + 1);
    strncpy(base64_data, data + start, end - start);
    base64_data[end - start - 1] = '\0';
    free(data);

    return base64_data;
}
__declspec(dllexport) int Verify(char* filepath, char* filesig, char* filepub)
{
    void* pubkey, * sigpad;
    size_t pubkey_len, sig_len, sigpad_len;
    uint8_t* tmpvv;
    size_t tmpvv_len;
    unsigned logn = 10;
    shake256_context rng;
    shake256_init_prng_from_system(&rng);

    pubkey_len = FALCON_PUBKEY_SIZE(logn);
    sig_len = FALCON_SIG_COMPRESSED_MAXSIZE(logn);
    sigpad_len = FALCON_SIG_PADDED_SIZE(logn);
    pubkey = xmalloc(pubkey_len);
    sigpad = xmalloc(sig_len);
    tmpvv_len = FALCON_TMPSIZE_VERIFY(logn);
    tmpvv = xmalloc(tmpvv_len);

    char* pdffile = readFromFile(filepath);
    pubkey = base64_to_bytes(read_pem_key(filepub));
    sigpad = base64_to_bytes(readFromFile(filesig));
    sigpad_len = FALCON_SIG_PADDED_SIZE(logn);

    int r = falcon_verify(sigpad, sigpad_len, FALCON_SIG_PADDED, pubkey, pubkey_len, pdffile, strlen(pdffile), tmpvv, tmpvv_len);
    if (r != 0) {
        return 1;
    }
    if (logn >= 5) {
        char* pdffile1 = pdffile;
        pdffile1[strlen(pdffile1) - 1]++;
        r = falcon_verify(sigpad, sigpad_len, FALCON_SIG_PADDED, pubkey, pubkey_len, pdffile1, strlen(pdffile1), tmpvv, tmpvv_len);
        if (r != FALCON_ERR_BADSIG) {
            return 1;
        }
    }
    return 0;
}

int main(int argc, char* argv[]) {
    if (argc != 4) {
        printf("Usage: ");
        printf("%s ", argv[0]);
        printf("<filepdf> <file sig> <file publickey>");
    }
    if (!Verify(argv[1], argv[2], argv[3])) {
        printf("Sucessfully");
    }
    else {
        printf("Fail");
    }
}