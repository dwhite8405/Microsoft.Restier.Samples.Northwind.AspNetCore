This is a copy of the Northwind example from https://github.com/odata/Restier. This allows you to test and develop OData clients without access to the Internet.

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/dwhite8405/tabular)

OData is a queryable REST service specification. https://www.odata.org/.

To run this locally, make sure you have the .NET SDK version 6.0 installed. This is the "dotnet-sdk-6.0" package on Debian-based distros. 

Then::

    $ dotnet build
    $ dotnet run

You should then see::

    info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:56488
    info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:56489
    info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/usbd/Microsoft.Restier.Samples.Northwind.AspNetCore

Connect your browser to either of those two URLs to view the OData service.

