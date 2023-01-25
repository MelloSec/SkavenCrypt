# SkavenCrypt

# Encryptor/Decryptor in C#

## XOR, AES, ALMOST RC4 encryption/decryption 
If you want to encrypt something you have the options of AES or XOR encryption of decryption. You give it the keyword/phrase to encrypt/decrypt the input file and optional output file. It can decrypt as well, just instead of -xor or -aes you do -dxor -daes

```
SkavenCrypt.exe -xor $keyword $inputFile (optional) $outputFile

SkavenCrypt.exe -dxor $keyword $inputFile (optional) $outputFile
```

## Encrypt:

![image](https://user-images.githubusercontent.com/65114647/214696506-9d146070-23f3-4943-afda-6ba9e3fde79f.png)

## Decrypt:

![image](https://user-images.githubusercontent.com/65114647/214698612-95a69aa1-b1f6-4ffa-baf0-97f95a6255a1.png)

## Check:

![image](https://user-images.githubusercontent.com/65114647/214698692-9e140f0d-ab02-4cdf-bc11-bd31e666e1f6.png)


```
SkavenCrypt.exe -aes $keyword $inputFile (optional) $outputFile

SkavenCrypt.exe -daes $keyword $inputFile (optional) $outputFile
```
## Encrypt:

![image](https://user-images.githubusercontent.com/65114647/214697858-f4fcc1f0-2519-4260-81c6-db18d4eaadfa.png)

## Decrypt
![image](https://user-images.githubusercontent.com/65114647/214697979-10cf2829-535b-4daf-b8bd-f7b652363788.png)


![image](https://user-images.githubusercontent.com/65114647/214698998-0ea039f6-1610-45cf-a116-0a717312748a.png)
![image](https://user-images.githubusercontent.com/65114647/214698753-882189ac-b730-464d-938b-f59b3f343777.png)






SkavenCrypt.exe -rc4 $keyword $inputFile (optional) $outputFile
(Broken Currently)



