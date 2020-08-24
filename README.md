# Run Api Nunit Tests in Docker:
**Prerequisites:**

Install Docker from here https://docs.docker.com/install/

```Be sure that Docker runs on Windows container!!!```

**Test docker:**
- Open powershell in administrator mode
- Execute docker run hello-world

**If it asks about login then:**
- Enter docker login
- Enter Docker ID & Password
- Execute docker run hello-world

**Create web app image:**
- Open Powershell as admin
- In PowerShell navigate to yourFolder\WebApp, type dir => you should see following structure. Current Dockerfile will be used to create image for WebApp

![](/images/1.png)

- To create image for webapp run:

   ```docker build -t webapp . ```
   
![](/images/2.png)

- Start container:

   ```docker run -it --rm -p 5000:81 --name appcontainer webapp```
   
 ![](/images/3.png)

   ``` For Windows containers, you need the IP address of the container (browsing to http://localhost:5000 won't work) This is known issue: https://docs.microsoft.com/en-    us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-3.1```


- Open another Powershell instance
- To display the IP address of the container run:

```docker exec appcontainer ipconfig```

The output from the command looks like this example:

![](/images/4.png)

- Copy the container IPv4 address (for example, 172.30.56.128) and paste into the browser address bar to test the app
- In Chrome open http://172.30.56.128/api/Product

**Run tests locally:** 

- Navigate to yourFolder\WebApp\WebApp.Tests and open WebApp.Tests.sln in VS. Current Dockerfile will be used to create image for WebApp.Tests

![](/images/5.png)

- Go to ProductsTests.cs class and change IP in url to your container IP address (from previous step) 

![](/images/6.png)

- Open powershell and navigate to 'yourFolder'\WebApp\WebApp.Tests

   ```NUnit Console is compiled using the full .NET Framework and does not currently support .NET Core. To run .NET Core tests from the command line, you need to use 'dotnet test'```

Run tests: 
   ```dotnet test --logger trx```
   
![](/images/7.png)

**Run tests from Docker:**
- Open another Powershell instance
- Navigate to 'yourFolder'\WebApp\WebApp.Tests
- To create an image for tests run:

   ```docker build -t tests .```
   
![](/images/8.png)

- Start container:

   ```docker run -it --rm --name testscontainer tests```

- Run tests:

   ```dotnet test --logger trx```
   
![](/images/9.png)
   




