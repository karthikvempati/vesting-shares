# vesting-shares
   
## Build and run

To build and run the vesting-shares, change to the *src/vesting-shares* directory and execute the following command:

```console
dotnet run
```

`dotnet run` builds the vesting-shares and runs the output executable. It implicitly runs `dotnet restore` to restore the dependencies of the project. 


To run the application with arguments, execute the following command:

```console
dotnet run arg1 arg2 arg3
```

`dotnet run test-files/example3.csv 2021-04-01 1` builds and runs the application and prints the output as per the format given in the requirement with 1 digit of precesion.