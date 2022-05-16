# Brute_Force

The program is written specifically for **Eluniver** for the purpose of interest :mortar_board:

One of the biggest problems with Brute Force is its running time. Due to the n-th number of possible password combinations, it can work for several minutes or forever. Therefore, you should not try to pick up a password using this program, you are more likely to grow old than hack a user account.

By the way, if the password length consists of 6 characters, while it is allowed to use the English alphabet from lower case and numbers, then there will be 1.4 billion possible passwords.

___
In order to set the characters that are used in the password, change this line

```c#
var password = "abcdefABCDEF123456";
```

To set the password length change this line

```c#
int size = 6;
```
