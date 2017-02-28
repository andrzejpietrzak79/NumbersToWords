This is the implementation of following coding task:

* Note that example 4 seems to be incorrect, however this implementation sticks to it.

# Task

Write a program which converts currency (dollars) from numbers into word presentation.
The maximum number is 999 999 999.
The maximum number of cents is 99.
Separator between dollars and cents is ‘,’ (comma).

Examples:

1. Input: 0

   Output: zero dollars
   
2. Input: 1 

   Output: one dollar
   
3. Input: 25,10 

   Output: twenty-five dollars and ten cents
   
4. Input: 0,1 

   Output: zero dollars and one cent
   
5. Input: 45 100 

   Output: forty-five thousand one hundred dollars
   
6. Input: 999 999 999,99 

   Output: nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents

Requirements:
* Use .NET framework 4.5 or higher with a client-server architecture.
* The client is to be implemented using WPF.
* Parsing and converting is to be implemented on the server side.
* Client-server communication should be implemented using WCF.
