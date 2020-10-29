# CheckoutKata

The solution consists of 2 ASP.NET projects:

## CheckoutKata

This is a Class Library targetting .NET Standard 2.1. It contains the logic to scan items and calculate total price inclusive of offers

## CheckoutKata.Tests

This contains the Unit test coverage for the **CheckoutKata** project. It references **CheckoutKata** as a dependency so it can call the methods on the library and do Assertions. It uses NUnit testing framework targetting .NET Core 3.1

To run the tests on the command line, please download the .NET Core SDK for your environment from https://dotnet.microsoft.com/download/dotnet-core/3.1. Navigate to the path of the project and run `dotnet test` (This will run dotnet restore and build commands implicitly)

```
E.g.
cd C:\code\CheckoutKata\CheckoutKata.Tests
dotnet test
```
