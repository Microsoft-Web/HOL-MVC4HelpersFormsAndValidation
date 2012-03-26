<a name="HOLTop" />
# ASP.NET MVC Helpers, Forms and Validation #
---

<a name="Overview" />
## Overview ##

>**Note:** This Hands-on Lab assumes you have basic knowledge of **ASP.NET MVC**. If you have not used **ASP.NET MVC** before, we recommend you to go over **ASP.NET MVC Fundamentals** Hand-on Lab.

In **ASP.NET MVC Models and Data Access** Hand-on Lab, you have been loading and displaying data from the database. In this Hands-on Lab, you will add to the **Music Store** application the ability to edit that data.

With that goal in mind, you will first create the controller that will support the Create, Read, Update and Delete (CRUD) actions of albums. You will generate an Index View template taking advantage of ASP.NET MVC’s scaffolding feature to display the albums’ properties in an HTML table. To enhance that view, you will add a custom HTML helper that will truncate long descriptions.

Afterwards, you will add the Edit and Create Views that will let you alter the albums in the database, with the help of form elements like dropdowns.

Lastly, you will let users delete an album and also you will prevent them from entering wrong data by validating their input.

<a name="Objectives" />
### Objectives ###

In this Hands-On Lab, you will learn how to:
- Create a controller to support CRUD operations
- Generate an Index View to display entity properties in an HTML table
- Add a custom HTML helper
- Create and customize an Edit View
- Differentiate between action methods that react to either HTTP-GET or HTTP-POST calls
- Add and customize a Create View
- Handle the deletion of an entity
- Validate user input

<a name="SystemRequirements" />
### System Requirements ###

You must have the following items to complete this lab:

- Visual Studio 11 Express Beta for Web


<a name="Setup" />
### Setup ###

#### Installing Code Snippets####
For convenience, much of the code you will be managing along this lab is available as Visual Studio code snippets. To install the code snippets run **.\Source\Assets\CodeSnippets.vsi** file.

####Installing Web Platform Installer####
This section assumes that you don't have some or all the system requirements installed. In case you do, you can simply skip this section.

Microsoft Web Platform Installer (WebPI) is a tool that manages the installation of the prerequisites for this Lab.

> **Note:** As well as the Microsoft Web Platform, WebPI can also install many of the open source applications that are available like Umbraco, Kentico, DotNetNuke and many more.  These are very useful for providing a foundation from which to customize an application to your requirements, dramatically cutting down your development time.

Please follow these steps to downloads and install Microsoft Visual Studio 11 Express Beta for Web:

1. Install **Visual Studio 11 Express Beta for Web**. To do this, Navigate to [http://www.microsoft.com/web/gallery/install.aspx?appid=VWD11_BETA&prerelease=true](http://www.microsoft.com/web/gallery/install.aspx?appid=VWD11_BETA&prerelease=true) using a web browser. 

	![Web Platform Installer 4.0 window](./images/Microsoft-Web-Platform-Installer-4.png?raw=true "Web Platform Installer 4.0 download")

	_Web Platform Installer 4.0 download_

1. The Web Platform Installer launches and shows Visual Studio 11 Express Beta for Web Installation. Click on **Install**.

 	![Visual Studio 11 Express Beta for Web Installer window](./images/Microsoft-VS-11-Install.png?raw=true "Visual Studio 11 Express Beta for Web Installer window")
 
	_Visual Studio 11 Express Beta for Web Installer window_

1. The **Web Platform Installer** displays the list of software to be installed. Accept by clicking **I Accept**.

 	![Web Platform Installer window](./images/Microsoft-Web-Platform-Installer-Prerequisites.png?raw=true "Web Platform Installer window")
 
	_Web Platform Installer window_

1. The appropriate components will be downloaded and installed.

 	![Web Platform Installation - Download progress](./images/Web-Platform-Installation-Download-progress.png?raw=true "Web Platform Installation - Download progress")
 
	_Web Platform Installation - Download progress_

1. The **Web Platform Installer** will resume downloading and installing the products. When this process is finished, the Installer will show the list of all the software installed. Click **Finish**.

 	![Web Platform Installer](./images/Web-Platform-Installer.png?raw=true "Web Platform Installer")
 
	_Web Platform Installer_

1. Finally the Web Platform Installer shows the installed products. Click **Finish** to finish the setup process.

---

<a name="Exercises" />
## Exercises ##
The following exercises make up this Hands-On Lab:

1.	[Creating the Store Manager controller and its Index view](#Exercise1)
1.	[Exercise 2: Adding an HTML Helper](#Exercise2)



> **Note:** Each exercise is accompanied by a starting solution. These solutions are missing some code sections that are completed through each exercise and therefore will not necessarily work if running them directly.
Inside each exercise you will also find an end folder where you find the resulting solution you should obtain after completing the exercises. You can use this solution as a guide if you need additional help working through the exercises.

Estimated time to complete this lab: **60 minutes**

<a name="Exercise1" />
### Exercise 1: Creating the Store Manager controller and its Index view###

In this exercise, you will learn how to create a new controller to support CRUD operations, customize its Index action method to return a list of albums from the database and finally generating an Index View template taking advantage of ASP.NET MVC’s scaffolding feature to display the albums’ properties in an HTML table.

<a name="Ex01Task1" />
####Task 1 - Creating the StoreManagerController####
In this task, you will create a new controller called **StoreManagerController** to support CRUD operations.

1. Start Microsoft Visual Studio 11 from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.
1. In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to **Source\Ex01-CreatingTheStoreManagerController\Begin**, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the last solution of the previous hands-on lab _Models and Data Access_.

1.	Follow these steps to install the **NuGet** package dependencies.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

	>**Note:** One of the advantages of using NuGet is that you don't have to ship all the libraries in your project, reducing the project size. With NuGet Power Tools, by specifying the package versions in the Packages.config file, you will be able to download all the required libraries the first time you run the project. This is why you will have to run these steps after you open an existing solution from this lab.
	
	>For more information, see this article: <http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages>.


1. Add the new controller. To do this, right-click the **Controllers** folder within the Solution Explorer, select **Add** and then the **Controller** command. Change the **Controller** **Name** to **StoreManagerController** and make sure the option **Controller with empty read/write actions** is selected. Click **Add**.

	 ![Add controller dialog](images/add-controller-dialog.png?raw=true "Add controller dialog")

	_Add Controller Dialog_

	A new Controller class is generated. Since you indicated to add actions for read/write, stub methods for those, common CRUD actions are created with TODO comments filled in, prompting to include the application specific logic.

<a name="Ex01Task4" />
#### Task 2 – Customizing the StoreManager Index####

In this task, you will customize the StoreManager Index action method to return a View with the list of albums from the database.

1.	In the StoreManagerController class, add a _using_ directive to MvcMusicStore.Models:

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 using MvcMusicStore_ – CSharp)

	<!-- mark:2 -->
	````C#
	using System.Web.Mvc;
	using MvcMusicStore.Models;
	````

1. Add a field to the **StoreManagerController** to hold an instance of **MusicStoreEntities.**

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 MusicStoreEntities_ – CSharp)

	<!-- mark:3 -->
	````C#
	public class StoreManagerController : Controller
	{
		MusicStoreEntities storeDB = new MusicStoreEntities();
	````

1.	Implement the StoreManagerController Index action to return a View with the list of albums. 

	The Controller action logic will be very similar to the StoreController’s Index action written earlier. Use LINQ to retrieve all albums, including Genre and Artist information for display.

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 StoreManagerController Index_ – CSharp)

	<!-- mark:6-10 -->
	````C#
	//
	// GET: /StoreManager/

	public ActionResult Index()
	{
		 var albums = storeDB.Albums
			  .Include("Genre").Include("Artist")
			  .ToList();

		 return View(albums);
	}

	````

<a name="Ex02Task3" />
#### **Task 3 - Creating the Index View**  ####

In this task, you will create the Index View template to display the list of albums returned by the **StoreManager** Controller.

1.	Before creating the new View template, you should build the project so that the **Add View Dialog** knows about the **Album** class to use. Select **Debug | Build MvcMusicSrore** to build the solution.
 
1.	Right-click inside the **Index** action method and select **Add View**. This will bring up the **Add View** dialog.

	![Add view](images/index-add-view.png?raw=true "Add view")
	
	_Adding a View from within the Index method_ 

1.	In the Add View dialog, verify that the View Name is **Index**. Select the **Create a strongly-typed view** option, and select **Album (MvcMusicStore.Models)** from the **Model class** drop-down. Select **List** from the **Scaffold** drop-down. Leave the **View engine** to **Razor**  and the other fields with their default value and then click **Add**.
	
	![Adding an index view](images/index-add-view-dialog.png?raw=true "Adding an index view")
 
	_Adding an Index View_


<a name="Ex01Task4" />
#### Task 4 - Customizing the scaffold of the Index View####

In this task, you will adjust the simple View template created with ASP.NET MVC scaffolding feature to have it display the fields you want.

>**Note:** The **scaffolding** support within ASP.NET MVC generates a simple View template which lists all fields in the Album model. **Scaffolding** provides a quick way to get started on a strongly typed view: rather than having to write the View template manually, scaffolding quickly generates a default template and then you can modify the generated code.

1.	Review the code created. The generated list of fields will be part of the following HTML table that **Scaffolding** is using for displaying tabular data.

	````HTML
	@model IEnumerable<MvcMusicStore.Models.Album>

	@{
		 ViewBag.Title = "Index";
		 Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Index</h2>

	<p>
		 @Html.ActionLink("Create New", "Create")
	</p>
	<table>
		 <tr>
			  <th>
					@Html.DisplayNameFor(model => model.GenreId)
			  </th>
			  <th>
					@Html.DisplayNameFor(model => model.ArtistId)
			  </th>
			  <th>
					@Html.DisplayNameFor(model => model.Title)
			  </th>
			  <th>
					@Html.DisplayNameFor(model => model.Price)
			  </th>
			  <th>
					@Html.DisplayNameFor(model => model.AlbumArtUrl)
			  </th>
			  <th></th>
		 </tr>

		@foreach (var item in Model) {
			 <tr>
				  <td>
						@Html.DisplayFor(modelItem => item.GenreId)
				  </td>
				  <td>
						@Html.DisplayFor(modelItem => item.ArtistId)
				  </td>
				  <td>
						@Html.DisplayFor(modelItem => item.Title)
				  </td>
				  <td>
						@Html.DisplayFor(modelItem => item.Price)
				  </td>
				  <td>
						@Html.DisplayFor(modelItem => item.AlbumArtUrl)
				  </td>
				  <td>
						@Html.ActionLink("Edit", "Edit", new { id=item.AlbumId }) |
						@Html.ActionLink("Details", "Details", new { id=item.AlbumId }) |
						@Html.ActionLink("Delete", "Delete", new { id=item.AlbumId })
				  </td>
			 </tr>
		}
	</table>
	````

1.	Replace the **\<table\>** code with the following code to display only the **Album Title**, **Artist**, and **Genre** fields. This deletes the **AlbumId**, **Price** and **Album Art URL** columns. Also, it changes GenreId and ArtistId columns to display their linked class properties of **Artist.Name** and **Genre.Name**, and removes the **Details** link.
	<!-- mark:1-27 -->
	````HTML
	<table>
		 <tr>
			  <th></th>
			  <th>Title</th>            
			  <th>Artist</th>            
			  <th>Genre</th>                
		 </tr>

		@foreach (var item in Model) {
		 <tr>
				<td>
					@Html.ActionLink("Edit", "Edit", new { id=item.AlbumId }) |
				
					@Html.ActionLink("Delete", "Delete", new { id=item.AlbumId })
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Title)
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Artist.Name)
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Genre.Name)
			  </td>
		 </tr>
		}
	</table>
	````

1.	Change the following descriptions.

	<!-- mark:3,6 -->
	````HTML	
	@model IEnumerable<MvcMusicStore.Models.Album>
	@{
		ViewBag.Title = "Store Manager - All Albums";
		Layout = "~/Views/Shared/_Layout.cshtml";
	}
	<h2>Albums</h2>

	````

1.	Also update the link to create a new Album changing its text.

	<!-- mark:1 -->
	````HTML
	@Html.ActionLink("Create New Album", "Create")
	````

<a name="Ex01Task5" />
#### Task 5 - Running the Application####
In this task, you will test that the **StoreManager** **Index** View template displays a list of albums according to the design of the previous steps.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager** to verify that a list of albums is displayed, showing their **Title**, **Artist** and **Genre**.

	![Browsing the list of albums](images/browsing-the-list-of-albums.png?raw=true "Browsing the list of albums")

	_Browswing the list of albums_

<a name="Exercise2" />
### Exercise 2: Adding an HTML Helper###

The StoreManager Index page has one potential issue: Title and Artist Name properties can both be long enough to throw off the table formatting. In this exercise you will learn how to add a custom HTML helper to truncate that text.

In the following figure, you can see how the format is modified because of the length of the text.

![Browsing the list of Albums with not truncated text](images/browsing-the-list-of-albums-with-not-truncate.png?raw=true "Browsing the list of Albums with not truncated text")

_Browsing the list of Albums with not truncated text_


<a name="Ex02Task1" />
#### Task 1 - Extending the HTML Helper####

In this task, you will add a new method **Truncate** to the **HTML** object exposed within ASP.NET MVC Views. To do this, you will implement an **extension method** to the built-in **System.Web.Mvc.HtmlHelper** class provided by ASP.NET MVC.

>**Note:** To read more about **Extension Methods**, please visit this msdn article. <http://msdn.microsoft.com/en-us/library/bb383977.aspx>.

1. Start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1.	In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex02-AddingAnHTMLHelper\Begin, select **MvcMusicStore.sln** and click **Open**.

1.	Add a new directory **Helpers** to place the new extension method. To do this, in the Solution Explorer, right-click the **MvcMusicStore** project, then **Add**, and **New Folder.**

	![Adding Helpers' folder](images/add-new-folder.png?raw=true "Adding Helpers' folder")
	
	_Adding Helpers' folder_

1.	Rename the folder to **Helpers**.
	![Helpers folder](images/helpers-folder.png?raw=true "Helpers folder")

	_Helpers folder_

1. Add a new class **HtmlHelpers** in the **Helpers** folder. To do this, right-click the **Helpers** folder, select **Add** and then **New Item**. In the **Add New Item** dialog, select the **Code** item under **Visual C#**, change the name to **HtmlHelpers.cs** and click **Add**.

	![Adding the Album class](images/adding-the-album-class.png?raw=true "Adding the Album class")
	
	_Adding the Album class_

1.	Add a using directive to **System.Web.Mvc** and a new method to the class. Because of extension methods definition, HtmlHelpers and the new method need to be static. Replace with the following code:

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex2 TruncateMethod_ - CSharp)

	<!-- mark:5-23 -->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;	
	using System.Web.Mvc;
	
	namespace MvcMusicStore.Helpers
	{
	    public static class HtmlHelpers
	    {
	        public static string Truncate(this HtmlHelper helper, string input, int length)
	        {
	            if (input.Length <= length)
	            {
	                return input;
	            }
	            else
	            {
	                return input.Substring(0, length) + "...";
	            }
	        }
	    }
	}
	````

<a name="Ex02Task2" />
#### Task 2 - Registering the HTML Helper####

In this task, you will register the HTML Helper with the application by modifying the application’s **Web.config** file.

1.	Open the **Web.config** file from the Solution Explorer.
1.	Add a reference to the **MvcMusicStore.Helpers** namespace in the **pages** section.

	<!-- mark:7 -->
	````XML
	<pages>
      <namespaces>
	     <add namespace="System.Web.Mvc" />
	     <add namespace="System.Web.Mvc.Ajax" />
	     <add namespace="System.Web.Mvc.Html" />
	     <add namespace="System.Web.Routing" />
        <add namespace="MvcMusicStore.Helpers" />
      </namespaces>
    </pages>
	````

	>**Note:**  Another aproach would be to add an import directive in all the pages that need it:
	>
	>````C#
	> @Import Namespace="MvcMusicStore.Helpers" 
	>````
	> Although, by adding the reference in the Web.config file, it will be available for all pages.
	>You will find more information about import directive in this msdn article <http://msdn.microsoft.com/en-us/library/eb44kack%28VS.71%29.aspx>.

<a name="Ex02Task03" />
#### Task 3 - Truncating Text in the Page####

In this task, you will use the **Truncate** method to truncate the text in the View template.

1. Open StoreManager's Index View. To do this, in the Solution Explorer expand the **Views** folder, then the **StoreManager** and open the **Index.aspx** file.
1. Replace the lines that show the Album's **Title** and **Artist Name**. To do this, replace the following lines:
	<!-- mark:9 -->
	````HTML
	<td>            
		@Html.Truncate(@Html.DisplayFor(modelItem => item.Title).ToHtmlString(),25)
	</td>
	<td>
		@Html.Truncate(@Html.DisplayFor(modelItem => item.Artist.Name).ToHtmlString(),25)
	</td>
	````
	
<a name="Ex02Task4" />
#### Task 4 - Running the Application####

In this task, you will test that the **StoreManager** **Index** View template truncates the Album’s Title and Artist Name.
1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager** to verify that long texts in the **Title** and **Artist** column **** are truncated.

![Truncated titles and artists names](images/truncated-titles-and-artists-names.png?raw=true "Truncated titles and artists names")

	_Truncated Titles and Artist Names_

---

<a name="Summary" />
## Summary ##

By completing this Hands-On Lab you have learned how to enable users to change the data stored in the database with the use of the following:

-	Controller actions like Index, Create, Edit, Delete
-	ASP.NET MVC’s scaffolding feature for displaying properties in an HTML table
-	Custom HTML helpers to improve user experience
-	Action methods that react to either HTTP-GET or HTTP-POST calls
-	A shared editor template for similar View templates like Create and Edit
- Form elements like drop-downs
- Data annotations for Model validation
- Client-side AJAX validation