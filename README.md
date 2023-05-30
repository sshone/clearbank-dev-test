# ClearBank - C# Developer Test
This repository contains a stubbed implementation of a payment processing system which I have refactored as part of an interview process for a Software Developer role.

I have created a **TestRunApplication** project which is there to demonstrate using Dependency Injection to run the code. 

Running this TestRunApplication project will currently always return a 'false' for the payment as the account retrieval logic is stubbed and the stubbed account returned contains no 'AllowedPaymentScheme', meaning validation fails.
