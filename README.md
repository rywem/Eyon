# Eyon

&copy; Ryan Wemmer 2019

## About
Project Goal: 
- Project Eyon is a project that:
	1. Creates a space for the exchange of cookbooks and recipes from different communities.
	2. Promotes, shares, and preserves the culinary richness of communities throughout the world.

### Installed Packages: 
1. Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 3.0.0  `for 'refresh' browser to update views when debugging`
	- Eyon.Site
2. Microsoft.AspNetCore.Mvc 2.2.0
	- Eyon.DataAccess
	- Eyon.Models
3. Microsoft.AspNetCore.Mvc.NewtonsoftJson 3.0.0
	- Eyon.Site
4. Microsoft.EntityFrameworkCore 3.0.0
	- Eyon.DataAccess
5. Microsoft.Extensions.Identity.Stores 3.0.0
	- Eyon.DataAccess
	- Eyon.Models
6. Microsoft.AspNetCore.Identity.EntityFrameworkCore
	- Eyon.Site
	- Eyon.DataAccess
7. Microsoft.EntityFrameworkcore.SqlServer 3.0.0
	- Eyon.DataAccess
	
### Database
- Note: database connection in Eyon.Site.appsettings.json
- Database Commands:
	- From Nuget Console, select Eyon.DataAccess project
		- `add-migration [name]`
		- `drop-database`
		- `update-database`

### Third Party Resources:
1. [Bootswatch](https://bootswatch.com/) - Bootstrap Wrapper Templates
2. [DataTables](https://datatables.net/) - Data table templates (includes CDN link)
3. [Toastr](https://codeseven.github.io/toastr/) - Simple javascript toast notifications        
4. [Sweet Alert](https://sweetalert.js.org/guides/) - Displays alerts
5. [Font Awesome](https://fontawesome.com/) - Beautiful, free icons
6. [jQuery Serializer JSON](https://cdnjs.com/libraries/jquery.serializeJSON) - jQuery Serializer
7. Images: 
	- [Unsplash](https://unsplash.com/) - Free images for use in projects. [License Details](https://unsplash.com/license)