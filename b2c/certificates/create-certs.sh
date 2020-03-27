#!/bin/bash
openssl genrsa 2048 > private_key.pem
openssl req -x509 -new -key private_key.pem -out public.pem

openssl pkcs12 -export -in public.pem -inkey private_key.pem -out b2c_client_certificate.pfx

openssl pkcs12 -in b2c_client_certificate.pfx -nodes | openssl x509 -noout -fingerprint
openssl pkcs12 -in b2c_client_certificate.pfx -nodes | openssl x509 -noout -subject
openssl pkcs12 -in b2c_client_certificate.pfx -nodes | openssl x509 -noout -issuer