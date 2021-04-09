# Tech Marketplace

The "Tech Marketplace" service is example of basic marketplace, where users can publish own electonical products or sell from others.

Features:
- Storing data in MSSQL DB
- Admin role, which can freeze products and ban users
- 7 basic categories (smartphones, laptops, etc.)
- Creating ads with large amount of properties to provide enough info about them
- Sort and filter for better search
- Notifications system where user get info about sales, purchases, price tracking and reports
- Using of 4 design patterns:
    - Facade (control the repositories subsystems, which are interracting with db)
    - Strategy (filter ads)
    - Observer (notifications system)
    - Chain of responsibility (report system)

![screenshots collage](/Marketplace/wwwroot/homeImg.png)
