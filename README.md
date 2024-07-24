<div align="center"> 
  <h3>myShop Pay</h3>
  <h6>Mock Payment Gateway<h6>
</div>

## Table of Contents
1. [About The Project](#about-the-project)
    - [Built with](#built-with)
2. [Screenshots](#screenshots)
3. [Related Projects](#related-projects)
4. [Getting Started](#getting-started)
5. [Licence](#licence)

## About The Project
The project is mock payment gateway, created for **myShop** project.
### Built with
* .NET 8
* ASP.NET Core Blazor
* MudBlazor - Blazor Component Library
* ASP.NET Core Web API
* Entity Framework Core (MS SQL)

## Screenshots
<div align="center">
  <img src="./img/payment.png" width="95%" alt="Payment"/>
  <p><strong>Payment</strong></p>
  <br />
  <br />
  <img src="./img/payment-mobile.png" width="30%" alt="Payment Mobile" />
  <p><strong>Payment Mobile</strong></p>
  <br />
  <br />
  <img src="./img/payment-accepted.png" width="95%" alt="Payment Accepted" />
  <p><strong>Payment Accepted</strong></p>
</div>

## Related Projects
* **[myShop API](https://github.com/marcin-niewczas/MyShop-API)**
* **[myShop Angular Client](https://github.com/marcin-niewczas/MyShop-Angular-Client)**

## Getting Started
> [!Important]
> For fully functionality, the **myShop Pay** project must have **[related projects](#related-projects)** running.
> 
> Full instruction for starting **myShop** projects is **[here](https://github.com/marcin-niewczas/MyShop-API#launch-myshop-projects)**.
1. Clone repository
   ```sh
   git clone https://github.com/marcin-niewczas/MyShop-Pay.git
   ```
2. Database
   - Windows
     - Nothing to do, but if you wanna run database via **Docker** go to `Mac OS/Linux` step
   - Mac OS/Linux
     - Go to `./MyShopPay/appsettings.json` and comment `WindowsConnectionString`, then uncomment `DockerConnectionString`
     - Run **Docker App**
     - In root directory of repository run
       ```sh
       cd ../../
       docker-compose up -d
       ```
3. In root directory of repository run
   ```sh
   dotnet run --project ./MyShopPay/MyShopPay.csproj --launch-profile https
   ```
## Licence
Distributed under the **MIT Licence**. See **[LICENSE](./LICENSE)** for more information.
