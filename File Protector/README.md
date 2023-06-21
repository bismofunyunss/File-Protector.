==================================================
                  File Protector v1.0
==================================================

Description
-----------
This program is designed to encrypt and decrypt files using custom password derived keys using the Argon2ID algorithm.

Argon2ID Parameters used:

Iterations: 50
Parallelism: All CPU cores of machine
Memory: 8MB
Salt Size: 64 Bytes
Hash Output Size: 32 Bytes

Usage
-----
You will need to register an account. Once an account is registered, you may log in using the credentials provided. Once logged in, you can
create a key using a password of your choice. Or you can generate a random password to use. Make sure you keep the key and don't forget it, you will need it to
decrypt any files that were encrypted with the key. When you enter the password, it will derive the bytes of that password and generate a key to use in order to
encrypy and decrypt files.

The level of security is high with the parameters set for Argon2. So depending on your machine, it may take awhile to generate keys and or log in.

A minimum of 16GB of RAM is required as well in order to run the cryptography algorithms.