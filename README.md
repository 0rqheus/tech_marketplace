# Marketplace

The "Tech Marketplace" service is example of basic marketplace, with ads, users and support members in it.

It has such features:
- Storing data in MSSQL DB
- 7 basic categories (smartphones, laptops, etc.)
- Creating ads with large amount of properties to provide enough info about them
- Sort and filter for better search
- Notifications system where user get info about sales, purchases, price tracking and reports
- Using of 7 design patterns (facade, decorator, strategy, observer)
    - Facade (control the repositories subsystems, which are interracting with db)
    - Decorator (for additions features for ad in view)
    - Proxy (control access in views)
    - Strategy (filter ads)
    - Observer (notifications system)
    - Chain of responsibility (report system)
    - Command (as handlers in chain)
    
- Admin role, which can freeze ads and ban users

TODO: Search, framework, SCSS + BEM
