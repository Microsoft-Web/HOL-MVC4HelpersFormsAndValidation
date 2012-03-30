<a name="HOLTop" />
# ASP.NET MVC 4 Helpers, Forms and Validation #
---

<a name="Overview" />
## Overview ##

In **ASP.NET MVC Models and Data Access** Hands-on Lab, you have been loading and displaying data from the database. In this Hands-on Lab, you will add to the **Music Store** application the ability to edit that data.

With that goal in mind, you will first create the controller that will support the Create, Read, Update and Delete (CRUD) actions of albums. You will generate an Index View template taking advantage of ASP.NET MVC’s scaffolding feature to display the albums’ properties in an HTML table. To enhance that view, you will add a custom HTML helper that will truncate long descriptions.

Afterwards, you will add the Edit and Create Views that will let you alter the albums in the database, with the help of form elements like dropdowns.

Lastly, you will let users delete an album and also you will prevent them from entering wrong data by validating their input.

>**Note:** This Hands-on Lab assumes you have basic knowledge of **ASP.NET MVC**. If you have not used **ASP.NET MVC** before, we recommend you to go over **ASP.NET MVC Fundamentals** Hands-on Lab.

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

1. [Creating the Store Manager controller and its Index view](#Exercise1)
1. [Adding an HTML Helper](#Exercise2)
1. [Creating the Edit View](#Exercise3)
1. [Adding a Create View](#Exercise4)
1. [Handling Deletion](#Exercise5)
1. [Adding Validation](#Exercise6)
1. [Using Unobtrusive jQuery at Client Side](#Exercise7)

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
1. In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to **Source\Ex01-CreatingTheStoreManagerController\Begin**, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the last solution of the previous hands-on lab _Models and Data Access with Database First_.

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

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 using MvcMusicStore_)

	<!-- mark:2 -->
	````C#
	using System.Web.Mvc;
	using MvcMusicStore.Models;
	````

1. Add a field to the **StoreManagerController** to hold an instance of **MusicStoreEntities.**

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 MusicStoreEntities_)

	<!-- mark:3 -->
	````C#
	public class StoreManagerController : Controller
	{
		MusicStoreEntities storeDB = new MusicStoreEntities();
	````

1.	Implement the StoreManagerController Index action to return a View with the list of albums. 

	The Controller action logic will be very similar to the StoreController’s Index action written earlier. Use LINQ to retrieve all albums, including Genre and Artist information for display.

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 StoreManagerController Index_)

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

1.	In the Add View dialog, verify that the View Name is **Index**. Select the **Create a strongly-typed view** option, and select **Album (MvcMusicStore.Models)** from the **Model class** drop-down. Select **List** from the **Scaffold template** drop-down. Leave the **View engine** to **Razor**  and the other fields with their default value and then click **Add**.
	
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

1.	In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex02-AddingAnHTMLHelper\Begin, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the end solution of Exercise 1.

1.	Follow these steps to install the **NuGet** package dependencies.

	>**Note:** You can skip these steps if you continued working with the solution of the previous exercise.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

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

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex2 TruncateMethod_)

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

1. Open StoreManager's Index View. To do this, in the Solution Explorer expand the **Views** folder, then the **StoreManager** and open the **Index.cshtml** file.
1. Replace the lines that show the Album's **Title** and **Artist Name**. To do this, replace the following lines:
	
	<!-- mark:1-7 -->
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
1.	The project starts in the Home page. Change the URL to **/StoreManager** to verify that long texts in the **Title** and **Artist** column are truncated.

	![Truncated titles and artists names](images/truncated-titles-and-artists-names.png?raw=true "Truncated titles and artists names")

	_Truncated Titles and Artist Names_



<a name="Exercise3" />
### Exercise 3: Creating the Edit View ###

In this exercise, you will learn how to create a form to allow store managers to edit an Album. They will browse the **/StoreManager/Edit/id** URL (**id** being the unique id of the album to edit), thus making an HTTP-GET call to the server.

The Controller Edit action method will retrieve the appropriate Album from the database, create a **StoreManagerViewModel** object to encapsulate it (along with a list of Artists and Genres), and then pass it off to a View template to render the HTML page back to the user. This page will contain a **\<form\>** element with textboxes and dropdowns for editing the Album properties.

Once the user updates the Album form values and clicks the **Save** button, the changes are submitted via an HTTP-POST call back to **/StoreManager/Edit/id**. Although the URL remains the same as in the last call, ASP.NET MVC identifies that this time it is an HTTP-POST and therefore executes a different Edit action method (one decorated with **[HttpPost]**).

<a name="Ex03Task1" />
#### Task 1 - Creating the StoreManagerViewModel####

In order to build and manage the intended Edit form, you will first need to pass to the View template an object with the following:

1.	An Album object that represents the current Album being edited
1.	A List of all Genres to populate the Genre dropdown list
1.	A List of all Artists to populate the Artist dropdown list


In this task, you will create a new StoreManagerViewModel class to help manage all of the above data. This class will be used within both Edit and Create action methods.

1. Start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.
1.	In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex03-CreatingTheEditView\Begin, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the solution of the previous exercise.

1.	Follow these steps to install the **NuGet** package dependencies.

	>**Note:** You can skip these steps if you continued working with the solution of the previous exercise.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**. You can alternativelly install the NuGet Power Tools from the **Manage NuGet Packages** window. 

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

1. Create the **StoreManagerViewModel** class. To do this, right-click on the **ViewModels** folder, select **Add** and then **Class**.

1. Name it **StoreManagerViewModel.cs** and click **Add.**

1. Add the following directions in the **StoreManagerViewModel.cs** class.

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex3 using System.Web.Mvc and MvcMusicStore.Models_)

	<!--mark: 5,6-->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using MvcMusicStore.Models;
	````

1.	Add the **Album**, **Artists** and **Genres** properties.

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex3 StoreManagerViewModel properties_)

	<!-- mark:3-5 -->
	````C#
    public class StoreManagerViewModel
    {
        public Album Album { get; set; }
        public SelectList Artists { get; set; }
        public SelectList Genres { get; set; }
    }
	````

	>**Note:** You are using **System.Web.Mvc** **SelectList** for Artists and Genres instead of the **System.Collections.Generic** List.
	>
	>**SelectList** is a cleaner way to populate HTML dropdowns and manage things like current selection. Instantiating and later setting up these ViewModel objects in the controller action will make the Edit form scenario cleaner.


<a name="Ex03Task2" />
#### Task 2 - Implementing the HTTP-GET Edit Action Method ####

In this task, you will implement the HTTP-GET version of the Edit action method to retrieve the appropriate Album from the database, as well as a list of all Genres and Artists. It will package this data up into the StoreManagerViewModel object defined in the last step, which will then be passed to a View template to render the response with.

1.	Open the **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.
1.	Add the following directive to **MvcMusicStore.ViewModels**. 
	
	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex3 using MvcMusicStore.ViewModels_)

	<!-- mark:2 -->
	````C#
	using MvcMusicStore.Models;
	using MvcMusicStore.ViewModels;
	````

1.	Replace the **HTTP-GET Edit** action method with the following code to retrieve the appropriate **Album** as well as the **Genres** and **Artists** lists.

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex3 StoreManagerController HTTP-GET Edit action_)

	<!-- mark:3-12 -->
	````C#
	public ActionResult Edit(int id)
	{
		Album album = storeDB.Albums.Single(a => a.AlbumId == id);

		var viewModel = new StoreManagerViewModel()
		{
			Album = album,
			Genres = new SelectList(storeDB.Genres.ToList(), "GenreId", "Name", album.GenreId),
			Artists = new SelectList(storeDB.Artists.ToList(), "ArtistId", "Name", album.ArtistId)
		};

		return View(viewModel);
	}
	````

<a name="Ex03Task3" />
#### Task 3 - Creating the Edit View####

In this task, you will create an Edit View template that will later display the album properties.

1.	Before creating the new View template, you should build the project so that the **Add View Dialog** knows about the class to use. Build the project by selecting the **Debug** menu item and then **Build MvcMusicStore**.

	![Building the project](images/building-the-project.png?raw=true "Building the project")

	_Building the project_

1. Create the Edit View. To do this, right-click inside the **Edit** action method and select **Add View**.
1.	In the Add View dialog, verify that the View Name is **Edit**. Check the **Create a strongly-typed view** checkbox and select **StoreManagerViewModel (MvcMusicStore.ViewModels)** from the **View data class** drop-down. Select **Edit** from the **Scaffold template** drop-down. Leave the other fields with their default value and then click **Add**.

	![Adding an Edit view](images/adding-an-edit-view.png?raw=true "Adding an Edit view")

	_Adding an Edit view_

1.	The generated Edit View template doesn’t include any fields because none of the properties in the StoreManagerViewModel are simple types like strings and integers.

	![edit-view-without-fields](images/edit-view-without-fields.png?raw=true)

	_Edit View without fields_

<a name="Ex03Task4" />
#### Task 4 - Customizing the Edit View ####

In this task, you will change the default View template to display the Album properties.

1.	Add an **Html.EditorFor()** helper to render a default HTML editing form for the Album. You can also change the legend to **Edit Album**.

	<!-- mark:1-4 -->
	````HTML
	<legend>Edit Album</legend>
	
	@Html.EditorFor(model => model.Album, 
	                        new { Artists = Model.Artists, Genres = Model.Genres })
	<p>
		<input type="submit" value="Save" />
	</p>
	````

	>**Note**: **Html.EditorFor()** helper will create a form that will enable the edition of its first parameter, in this case an **Album**. Additionally, you are passing a second parameter which is optional, that contains the list of **Artists** and **Genres**. This will be used later on.


<a name="Ex03Task5" />
#### Task 5 - Running the Application ####

In this task, you will test that the **StoreManager** **Edit** View page displays the properties' values for the album passed as parameter.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager/Edit/388** to verify that the properties' values for the album passed are displayed.

	![Browsing Album's Edit View](images/browsing-albums-edit-view.png?raw=true "Browsing Album's Edit View")

	_Browsing Album's Edit view_

<a name="Ex03Task6" />
#### Task 6 - Creating a Shared Album Editor Template ####

Since the exact same form fields for Album Edit will be needed to handle the Album Create case, in this task you will create a Shared Album Editor template that will take care of both cases with the same code.

1.	Close the browser if needed, to return to the Visual Studio window. Create a folder inside **/Views/Shared** folder and name it **EditorTemplates**.

	
	![Adding a new folder](images/add-new-folder-shared.png?raw=true "Adding a new folder")

	_Adding a new folder_

1. Right-click the **EditorTemplates** folder, select **Add** and then **View**.

	![Adding a view template](images/adding-a-view-template.png?raw=true "Adding a view template")

	_Adding a View template_

1.	This will be a Partial View, meaning that it is intended to be displayed inside another view. In the Add View dialog, change the Name to **Album**. Select the **Create as partial view** and the **Create a strongly-typed view** options. Select **Album (MvcMusicStore. Models)** from the **Model class** drop-down and select **Edit** from the **Scaffold tempalte** drop-down. Leave the other fields with their default value and then click **Add**.

	![Adding a partial view](images/adding-a-partial-view.png?raw=true "Adding a partial view")

	_Adding a partial view_


<a name="Ex03Task4" />
#### Task 7 - Implementing drop-downs on the Album Editor Template ####

In this task, you will add drop-downs to the View template created in the last task, so that the user can select from a list of Artists and Genres.

1.	Replace all the **Album** Partial View code with the following:

	<!-- mark:4-26 -->
	````HTML
	@model MvcMusicStore.Models.Album
	
		<p>
			@Html.LabelFor(model => model.Title)
			@Html.TextBoxFor(model => model.Title)
			Html.ValidationMessageFor(model => model.Title)
		</p>            
		<p>
			@Html.LabelFor(model => model.Price) 
			@Html.TextBoxFor(model => model.Price) 
			@Html.ValidationMessageFor(model => model.Price)
		</p>            
		<p>
			@Html.LabelFor(model => model.AlbumArtUrl)
			@Html.TextBoxFor(model => model.AlbumArtUrl)
			@Html.ValidationMessageFor(model => model.AlbumArtUrl)
		</p>            
		<p>
			@Html.LabelFor(model => model.Artist)
			@Html.DropDownList("ArtistId", (SelectList) ViewData["Artists"])
		</p>            
		<p>
			@Html.LabelFor(model => model.Genre)
			@Html.DropDownList("GenreId", (SelectList) ViewData["Genres"])
		</p>           
	````

	>**Note:** An **Html.DropDownList** helper has been added to render drop-downs for choosing Artists and Genres. The parameters passed to **Html.DropDownList** are:
	>
	>1. The name of the form field (**"ArtistId"**)
	>1. The **SelectList** of values for the drop-down. **ViewData** is actually the object taken from the **Html.EditorFor** second parameter you included in task 4:
	>
	> ````C#
	>@Html.EditorFor(model => model.Album, 
	                        new { Artists = Model.Artists, Genres = Model.Genres })
	>````
	>
	>The **Html.EditorFor()** helper will create a form that will enable the edition of its first parameter, in this case an **Album**. Additionally, you are passing a second parameter which is optional, that contains the list of **Artists** and **Genres**. This will be used later on.

<a name="Ex03Task8" />
#### Task 8 - Running the Application ####

In this task, you will test that the **StoreManager** **Edit** View page displays drop-downs instead of Artist and Genre ID text fields.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager/Edit/388** to verify that it displays drop-downs instead of Artist and Genre ID text fields.

	![Browsing Album's Edit View with drop downs](images/browsing-albums-edit-view-with-drop-downs.png?raw=true "Browsing Album's Edit View with drop downs")

	_Browsing Album’s Edit view, this time with dropdowns_


<a name="Ex03Task9" />
#### Task 9 - Implementing the HTTP-POST Edit action method ####

Now that the Edit View displays as expected, you need to implement the HTTP-POST Edit Action method to save the changes made to the Album.

1.	Close the browser if needed, to return to the Visual Studio window. Open **StoreManagerController** from the **Controllers** folder.
1.	Replace **HTTP-POST Edit** action method code with the following (note that the method that must be replaced is overloaded version that receives two parameters):

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex3 StoreManagerController HTTP-POST Edit action_)
	
	<!-- mark:4-27 -->
	````C#
	[HttpPost]
	public ActionResult Edit(int id, FormCollection collection)
	{
		 var album = storeDB.Albums.Single(a => a.AlbumId == id);

		 try
		 {
			  // Save Album

			  UpdateModel(album, "Album");
			  storeDB.SaveChanges();

			  return RedirectToAction("Index");
		 }
		 catch
		 {
			  // Error occurred - so redisplay the form

			  var viewModel = new StoreManagerViewModel()
			  {
					Album = album,
					Genres = new SelectList(storeDB.Genres.ToList(), "GenreId", "Name", album.GenreId),
					Artists = new SelectList(storeDB.Artists.ToList(), "ArtistId", "Name", album.ArtistId)
			  };

			  return View(viewModel);
		 }
	}
	````

	>**Note:** This method will be executed when the user clicks the **Save** button of the View and performs an HTTP-POST of the form values back to the server to persist them in the database. The decorator **[HttpPost]** indicates that the method should be used for those HTTP-POST scenarios.
	>The method takes an **id** (read from the route parameter values) and a **FormCollection** (read from the HTML Form).
	>
	>The method will perform three steps:
	>
	>1. Load the existing album object from the database with the **id** passed as parameter
	>1. Try to update the album using the values posted from the client, using the Controller’s built-in **UpdateModel** method. The **UpdateModel** method actually relies on the **ModelBinder** (MVC 2.0) to identify that you are actually updating a **StoreManagerViewModel** instance. Since that **ViewModel** holds an **Album** object and also 2 **SelectLists** for **Artists** and **Genres**, the key **“Album”** you are including as **UpdateModel**’s second parameter, refers to the need of updating the **“Album”** property of the Model.
	>1. Display results back to the user - either by redisplaying the form in case of an error, or by redirecting back to the list of albums in case of a successful update.


<a name="Ex03Task10" />
#### Task 10 - Running the Application ####

In this task, you will test that the **StoreManager Edit** View page actually saves the updated Album data in the database.

1.	Press **F5** to run the Application.

1.	The project starts in the Home page. Change the URL to **/StoreManager/Edit/388**. Change the Album title to **Greatest Hits Vol. 1** and click on **Save**. Verify that album’s title actually changed in the list of albums.
	
	![Updating an album](images/updating-an-album.png?raw=true "Updating an album")

	_Updating an Album_


<a name="Exercise4" />

### Exercise 4: Adding a Create View###

Now that the **StoreManagerController** supports the **Edit** ability, in this exercise you will learn how to add a Create View template to let store managers to add new Albums to the application.

Like you did with the Edit functionality, you will implement the Create scenario using two separate methods within the **StoreManagerController** class:

1. One action method will display an empty form when store managers first visit the **/StoreManager/Create** URL. 

1.	A second action method will handle the scenario where the store manager clicks the **Save** button within the form and submits the values back to the **/StoreManager/Create** URL as an HTTP-POST.

	
<a name="Ex04Task1" />
#### Task 1 - Implementing the HTTP-GET Create action method ####

In this task, you will implement the HTTP-GET version of the Create action method to retrieve a list of all Genres and Artists, package this data up into a StoreManagerViewModel object, which will then be passed to a View template.

1. Start Visual Studio 11 Express Beta for Web from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1.	In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex04-AddingACreateView\Begin, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the solution of the previous exercise.

1.	Follow these steps to install the **NuGet** package dependencies.

	>**Note:** You can skip these steps if you continued working with the solution of the previous exercise.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**. You can alternativelly install the NuGet Power Tools from the **Manage NuGet Packages** window. 

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

1.	Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.

1.	Replace the **Create** action method code with the following:

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex4 StoreManagerController HTTP-GET Create action_)

	<!-- mark:6-11 -->
	````C#	
	//
	// GET: /StoreManager/Create

	public ActionResult Create()
	{
		var viewModel = new StoreManagerViewModel()
		{
			 Album = new Album(),
			 Genres = new SelectList(storeDB.Genres.ToList(), "GenreId", "Name"),
			 Artists = new SelectList(storeDB.Artists.ToList(), "ArtistId", "Name")
		};

		return View(viewModel);
	}
	````

<a name="Ex04Task2" />
#### Task 2 - Adding the Create View ####

In this task, you will add the Create View template that will display a new (empty) Album form.

1.	Before creating the new View template, you should build the project so that the **Add View Dialog** knows about the class to use. Build the project by selecting the **Debug** menu item and then **Build MvcMusicStore**.

	![Building the project](images/building-the-project.png?raw=true "Building the project")

	_Building the project_

1.	Right-click inside the **Create** action method and select **Add View**. This will bring up the Add View dialog.
1.	In the Add View dialog, verify that the View Name is **Create**. Select the **Create a strongly-typed view** option and select **StoreManagerViewModel (MvcMusicStore.ViewModels)** from the **Model class** drop-down and **Create** from the **Scaffold template** drop-down. Leave the other fields with their default value and then click **Add**. 


	![Adding a create view](images/adding-a-create-view.png?raw=true "adding-a-create-view.png")

	_Adding the Create View_


1.	Update the generated View template to invoke the **Html.EditorFor()** helper method:

	<!-- mark:2-4 -->
	````HTML
	<fieldset>
        <legend>Create Album</legend>
        @Html.EditorFor(model => model.Album, 
                    new { Artists = Model.Artists, Genres = Model.Genres })
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
	````

<a name="Ex04Task3" />
#### Task 3 - Running the Application ####

In this task, you will test that the **StoreManager** **Create** View page displays an empty Album form.

1. Press **F5** to run the Application.
1. The project starts in the Home page. Change the URL to **/StoreManager/Create**. Verify that an empty form is displayed for filling the new Album properties.
	
	![Create View with an empty form](images/create-view-empty-form.png?raw=true "Create View with an empty form")

	_Create View with an empty form_


<a name="Ex04Task4" />
#### Task 4 - Implementing the HTTP-POST Create Action Method ####

In this task, you will implement the HTTP-POST version of the Create action method that will be invoked when a user clicks the **Save** button. The method should save the new album in the database.

1.	Close the browser if needed, to return to the Visual Studio window. Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.
1.	Replace **HTTP-POST Create** action method code with the following:

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex4 StoreManagerController HTTP- POST Create action_)

	<!-- mark:2-28 -->
	````C#
	[HttpPost]        
	public ActionResult Create(Album album)
	{
		try
		{
			if (ModelState.IsValid)
			{
				// Save Album
             storeDB.AddToAlbums(album);
             storeDB.SaveChanges();

             return Redirect("Index");
			}
		}
		catch (Exception ex)
		{
			ModelState.AddModelError(String.Empty, ex);
		}

		// Invalid - redisplay with errors
		var viewModel = new StoreManagerViewModel()
		{
			Album = album,
			Genres = new SelectList(storeDB.Genres.ToList(), "GenreId", "Name", album.GenreId),
			Artists = new SelectList(storeDB.Artists.ToList(), "ArtistId", "Name", album.ArtistId)
		};

		return View(viewModel);
	}
	````

	>**Note:** One difference from the previous Edit action method is that instead of loading an existing Album and calling UpdateModel, the action method is receiving an Album as a parameter. ASP.NET MVC will automatically create this Album object from the posted \<form\> values. 
	>The method checks that the submitted values are valid, and if they are, it saves the new Album in the database and then redirects to the StoreManager Index View.


<a name="Ex04Task5" />
#### Task 5 - Running the Application ####

In this task, you will test that the **StoreManager Create** View page lets you create a new Album and then redirects to the StoreManager Index View.

1. Press **F5** to run the Application.
1. The project starts in the Home page. Change the URL to **/StoreManager/Create**. Fill the empty form with data for a new Album, like the one in the following figure:

	![Creating an Album](images/creating-an-album.png?raw=true "Creating an Album")

	_Creating an Album_

1. Verify that you get redirected to the StoreManager Index View that includes the new Album just created.


	![New Album Created](images/new-album-created.png?raw=true "New Album Created")

	_New Album Created_


<a name="Exercise5" />
### Exercise 5: Handling Deletion ###

The ability to delete albums is not yet implemented. This is what this exercise will be about. Like before, you will implement the Delete scenario using two separate methods within the **StoreManagerController** class:

1. One action method will display a confirmation form
1. A second action method will handle the form submission
	
<a name="Ex05Task1" />
#### Task 1 - Implementing the HTTP-GET Delete Action Method ####

In this task, you will implement the HTTP-GET version of the Delete action method to retrieve the album’s information.

1. Start Microsoft Visual Studio 11 from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.
1. In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to **Source\Ex05-HandlingDeletion\Begin**, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the last solution.

1.	Follow these steps to install the **NuGet** package dependencies.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

1. Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.
1. The Delete controller action is exactly the same as the previous Store Details controller action: it queries the **album** object from the database using the **id** provided in the URL and returns the appropiate **View**. To do this, replace the HTTP-GET **Delete** action method code with the following:


	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex5 Handling Deletion HTTP-GET Delete action_)

	<!-- mark:5-7 -->
	````C#
	//
	// GET: /StoreManager/Delete/5	
	public ActionResult Delete(int id)
	{
	    var album = storeDB.Albums.Single(a => a.AlbumId == id);
	
	    return View(album);
	}
	````

1.	Before creating the new View template, you should build the project so that the **Add View Dialog** knows about the class to use. Build the project by selecting the **Debug** menu item and then **Build MvcMusicStore**.

	![Building the Project](images/building-the-project.png?raw=true "Building the Project")

	_Building the project_

1.	Right-click inside the **Delete** action method and select **Add View**. This will bring up the Add View dialog.

1.	In the Add View dialog, verify that the View name is **Delete**. Select the **Create a strongly-typed view** option and select **Album (MvcMusicStore.Models)** from the **Model class** drop-down. Select **Delete** from the **Scaffold template** drop-down. Leave the other fields with their default value and then click **Add**.

	![Adding a Delete View](images/adding-a-delete-view.png?raw=true "Adding a Delete View")

	_Adding a Delete View_

1.	The Delete template shows all the fields from the model. You will show only the album’s title. To do this, replace the content of the view with the following code:

	<!-- mark:5-14 -->
	````HTML
	@model MvcMusicStore.Models.Album
	@{
		 ViewBag.Title = "Delete";
	}
	<h2>Delete Confirmation</h2>

	<h3> Are you sure you want to delete the album title <strong>@Model.Title </strong> ? </h3>

	@using (Html.BeginForm()) {
		 <p>
			  <input type="submit" value="Delete" /> |
			  @Html.ActionLink("Back to List", "Index")
		 </p>
	}
	````

<a name="Ex05Task2" />
#### Task 2 - Running the Application####

In this task, you will test that the **StoreManager** **Delete** View page displays a confirmation deletion form.
1.	Press **F5** to run the Application.

1.	The project starts in the Home page. Change the URL to **/StoreManager**. Select one album to delete by clicking **Delete** and verify that the new view is uploaded.

	![Deleting an Album](images/deleting-an-album.png?raw=true "Deleting an Album")

	_Deleting an Album_


<a name="Ex05Task3" />
#### Task 3- Implementing the HTTP-POST Delete Action Method ####

In this task, you will implement the HTTP-POST version of the Delete action method that will be invoked when a user clicks the **Delete** button. The method should delete the album in the database.

1.	Close the browser if needed, to return to the Visual Studio window. Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.
1.	Replace **HTTP-POST Delete** action method code with the following:

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Handling Deletion HTTP-POST Delete action_)

	<!-- mark:7-12 -->
	````C#
	//
	// POST: /StoreManager/Delete/5
	
	[HttpPost]
	public ActionResult Delete(int id, FormCollection collection)
	{
	    var album = storeDB.Albums
	        .Include("OrderDetails").Include("Carts")
	        .Single(a => a.AlbumId == id);
	
	    storeDB.DeleteObject(album);
	    storeDB.SaveChanges();
	
	    return RedirectToAction("Index");	
	}
	````

<a name="Ex05Task4" />
#### Task 4 - Deleting on Cascade ####

Because of Referential Integrity, a deletion of an **Album** could raise an exception if it has **OrderDetails** entries. To solve this, you should allow cascade deletes. So, when you delete an Album, it will delete all the OrderDetails entries for that album. In this task, you will activate deletion on cascade.

>**Note:** In this scenario you will delete an Album no matter if it has an order associated. Consider that in other application scenarios this could not be the correct action.

1.	On the Solution Explorer expand the **Models** folder and then double-click **StoreDB.edmx**. This opens the Entity Data Model designer.
1.	Open the **Models/StoreDB.edmx** entity diagram. Right-click the relation between **Album** and **OrderDetail** and select **Properties**.
	
	![Editing properties on a relation](images/editing-properties-on-a-relation.png?raw=true)

	_Editing Properties on a Relation_

1. In the **Properties** window, set the **End1 OnDelete** value to **Cascade**.

	![OnDelete property](images/ondelete-property.png?raw=true "OnDelete property")

	_OnDelete property_

<a name="Ex05Task5" />
#### Task 5 - Running the Application ####

In this task, you will test that the **StoreManager Delete** View page lets you delete an Album and then redirects to the StoreManager Index View.

1. Press **F5** to run the Application.
1. The project starts in the Home page. Change the URL to **/StoreManager**.  Select one album to delete by clicking **Delete.** Confirm the deletion by clicking **Delete** button:

	![Deleting an Album](images/deleting-an-album-confirmation.png?raw=true "Deleting an Album")

	_Deleting an Album_ 

1. Verify that the album was deleted since it does not appear in the **Index** page.


<a name="Exercise6" />
### Exercise 6: Adding Validation ###

Currently, the Create and Edit forms you have in place do not perform any kind of validation. If the user leaves a required field blank or type letters in the price field, the first error you will get will be from the database.

You can add validation to the application by adding Data Annotations to your model class. Data Annotations allow describing the rules you want applied to your model properties, and ASP.NET MVC will take care of enforcing and displaying appropriate message to users.

<a name="Ex06Task1" />
#### Task 1 - Adding Data Annotations ####

In this task, you will add Data Annotations to the Album Model that will make the Create and Edit page display validation messages when appropriate.

1. Start Microsoft Visual Studio 11 from **Start** | **All Programs** | **Microsoft Visual Studio 11 Express** | **Visual Studio 11 Express Beta for Web**.

1.	In the **File** menu, choose **Open Project**. In the Open Project dialog, browse to Source\Ex06-AddingValidation\Begin, select **MvcMusicStore.sln** and click **Open**. You can alternativelly continue working with the solution of the previous exercise.

1.	Follow these steps to install the **NuGet** package dependencies.

	>**Note:** You can skip these steps if you continued working with the solution of the previous exercise.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**. You can alternativelly install the NuGet Power Tools from the **Manage NuGet Packages** window. 

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

1.	Add a new metadata partial class. To do this, right-click the **Models** folder within the Solution Explorer, select **Add** and then **Class**. Name it **Album.cs** and click **OK**.

	>**Note:** For a simple Model class, adding a Data Annotation is just handled by adding a **using** statement for **System.ComponentModel.DataAnnotation**, then placing a **[Required]** attribute on the appropriate properties.
	>The following example would make the **Name** property a required field in the View.
	>
	>````C#
	>using System.ComponentModel.DataAnnotations;
	>namespace SuperheroSample.Models
	>{
	>    public class Superhero
	>    {
	>        [Required]
	>        public string Name { get; set; }
	>        public bool WearsCape { get; set; }
	>    }
	>}
	>````
	>
	>This is a little more complex in cases like this application where the Entity Data Model is generated. If you added Data Annotations directly to the model classes, they would be overwritten if you update the model from the database.
	>Instead, you can make use of metadata partial classes which will exist to hold the annotations and are associated with the model classes using the **\[MetadataType\]** attribute.

1.	Replace **Album.cs** content with the highlighted code, so that it looks like the following:

	>**Note:** The line **\[DisplayFormat(ConvertEmptyStringToNull=false)\]** indicates that empty strings from the model won’t be converted to null when the data field is updated in the data source. 
	>This setting will avoid an exception when the Entity Framework assigns null values to the model before Data Annotation validates the fields.

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex6 Album metadata partial class_)

	<!-- mark:5-42 -->
	````C#
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;
	
	namespace MvcMusicStore.Models
	{
	    [MetadataType(typeof(AlbumMetaData))]
	    public partial class Album
	    {
	        // Validation rules for the Album class
	
	        [Bind(Exclude = "AlbumId")]
	        public class AlbumMetaData
	        {
	            [ScaffoldColumn(false)]
	            public object AlbumId { get; set; }
	
	            [DisplayName("Genre")]
	            public object GenreId { get; set; }
	
	            [DisplayName("Artist")]
	            public object ArtistId { get; set; }
	
	            [Required(ErrorMessage = "An Album Title is required")]
	            [DisplayFormat(ConvertEmptyStringToNull = false)]
	            [StringLength(160)]
	            public object Title { get; set; }
	
	            [DisplayName("Album Art URL")]
	            [StringLength(1024)]
	            public object AlbumArtUrl { get; set; }
	
	            [Required(ErrorMessage = "Price is required")]
	            [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
	            public object Price { get; set; }
	        }
	    }
	}
	````

	>**Note:** This **Album** partial class has a **MetadataType** attribute which points to the **AlbumMetaData** class for the Data Annotations. These are some of the Data Annotation attributes you are using to annotate the Album model:
	>
	>- Required - Indicates that the property is a required field
	>-  DisplayName - Defines the text to be used on form fields and validation messages
	>-  DisplayFormat - Specifies how data fields are displayed and formatted.
	>-  StringLength - Defines a maximum length for a string field
	>-  Range - Gives a maximum and minimum value for a numeric field
	>-  Bind - Lists fields to exclude or include when binding parameter or form values to model properties
	>-  ScaffoldColumn - Allows hiding fields from editor forms

<a name="Ex06Task2" />
#### Task 2 - Running the Application ####

In this task, you will test that the Create and Edit pages validate fields, using the display names chosen in the last task.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager/Create**.  Verify that the display names match the ones in the partial class (like **Album Art URL** instead of **AlbumArtUrl**)
1.	Click **Save**, without filling the form. Verify that you get the corresponding validation messages.

	![Validated fields in the Create page](images/validated-fields-in-create-page.png?raw=true "Validated fields in the Create page")

	_Validated fields in the Create page_

1.	You can verify that the same occurs with the **Edit** page. Change the URL to **/StoreManager/Edit/388** and verify that the display names match the ones in the partial class (like **Album Art URL** instead of **AlbumArtUrl**). Empty the **Title** and **Price** fields and click **Save**. Verify that you get the corresponding validation messages.

	![Validated fields in the Edit page](images/validated-fields-in-edit-page.png?raw=true)

	_Validated fields in the Edit page_

<a name="Exercise7" /> 
### Exercise 7: Using Unobtrusive jQuery at Client Side ###

In this exercise, you will learn how to enable MVC 4 Unobtrusive jQuery validation at client side.

> **Note:** The Unobtrusive jQuery uses data-ajax prefix JavaScript to invoke action methods on the server rather than intrusively emitting inline client scripts.

<a name="Ex7Task1" />
#### Task 1 - Running the Application before Enabling Unobtrusive jQuery ####

In this task, you will run the application before including jQuery in order to compare both validation models.

1. Open the begin solution **MvcMusicStore.sln** at **Source\Ex07-UnobtrusivejQueryValidation\Begin**.

1.	Follow these steps to install the **NuGet** package dependencies.

	>**Note:** You can skip these steps if you continued working with the solution of the previous exercise.

	1.	Open the **NuGet** **Package Manager Console**. To do this, select **Tools | Library Package Manager | Package Manager Console**.

	1.	In the **Package Manager Console,** type **Install-Package NuGetPowerTools**.

	1.	After installing the package, type **Enable-PackageRestore**.

	1.	Build the solution. The **NuGet** dependencies will be downloaded and installed automatically.

1. Press **F5** to run the application.

1. The project starts in the Home page. Browse **/StoreManager/Create** and click **Save** without filling the form to verify that you get validation messages:

 	![Client validation disabled](./images/Client-validation-disabled.png?raw=true "Client validation disabled")
 
	_Client validation disabled_

1. In the browser, open the **Create** view source code:

	````HTML
	...
	<form action="/StoreManager/Create" id="form0" method="post">
	<div class="validation-summary-errors" id="validationSummary">
		<ul>
			<li style="display:none"></li>
		</ul>
	</div>    
	<fieldset>
        <legend>Create Album</legend> 
		 <p>
			  <label for="Album_Title">Title</label>
			  <input class="input-validation-error" id="Album_Title" name="Album.Title" type="text" value="" />
			  <span class="field-validation-error" id="Album_Title_validationMessage">An Album Title is required</span>
		 </p>            
		 <p>
			  <label for="Album_Price">Price</label> 
			  <input class="input-validation-error" id="Album_Price" name="Album.Price" type="text" value="0" /> 
			  <span class="field-validation-error" id="Album_Price_validationMessage">Price must be between 0.01 and 100.00</span>
		 </p>
	...
	````

<a name="Ex7Task2" />
#### Task 2 - Enabling Unobtrusive Client Validation ####

In this task, you will enable jQuery **unobtrusive client validation** from **Web.config** file, which is by default set to false in all new ASP.NET MVC 4 projects.
Additionally, you will add the necessary scripts references to make jQuery Unobtrusive Client Validation work.

1. Open **Web.Config** file at project root, and make sure that the **ClientValidationEnabled** and **UnobtrusiveJavaScriptEnabled** keys values are set to **true**.
	
	<!-- mark:7-8 -->
	````XML
	...
	<configuration>
	  <appSettings>
		 <add key="webpages:Version" value="2.0.0.0" />
		 <add key="webpages:Enabled" value="true" />
		 <add key="PreserveLoginUrl" value="true" />
		 <add key="ClientValidationEnabled" value="true" />
		 <add key="UnobtrusiveJavaScriptEnabled" value="true" />
	</appSettings>
	...
	````
	
	> **Note:** You can also enable client validation by code at Global.asax.cs to get the same results: 
	>
	> **HtmlHelper.ClientValidationEnabled = true;**
	>
	> Additionally, you can assign ClientValidationEnabled attribute into any controller to have a custom behavior.

1. Open **Create.cshtml** at **Views\StoreManager**.

1. Make sure the following script files, **jquery.validate** and  **jquery.validate.unobtrusive**, are referenced in the view.

	<!-- mark:4-5 -->
	````CSHTML
	...
	<h2>Create</h2>

	<script src="@Url.Content("~/Scripts/jquery.validate.min.js")"></script>
	<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")"></script>
	````

	> **Note:** All these jQuery libraries are included in MVC 4 new projects. You can find more libraries in the **/Scripts** folder of you project.
	>
	> In order to make this validation libraries work, you need to add a reference to the jQuery framework library. Since this reference is already added in the **_Layout.cshtml** file, you do not need to add it in this particular view.

<a name="Ex7Task3" />
#### Task 3 - Running the Application Using Unobtrusive jQuery Validation ####

In this task, you will test that the **StoreManager** create view template performs client side validation using jQuery libraries when the user creates a new album.

1. Press **F5** to run the application.

1. The project starts in the Home page. Browse **/StoreManager/Create** and click **Save** without filling the form to verify that you get validation messages:

 	![Client validation with jQuery enabled](./images/Client-validation-with-jQuery-enabled.png?raw=true "Client validation with jQuery enabled")
 
	_Client validation with jQuery enabled_

1. In the browser, open the source code for Create view:

	````HTML
	...
	<h2>Create</h2>

	<form action="/StoreManager/Create" method="post">
	<div class="validation-summary-errors">
		<ul>
			<li style="display:none"></li>
		</ul>
	</div>
	<fieldset>
		<legend>Create Album</legend>			  
		<p>
			  <label for="Album_Title">Title</label>
			  <input class="input-validation-error" data-val="true" data-val-length="The field Title must be a string with a maximum length of 160." data-val-length-max="160" data-val-required="An Album Title is required" id="Album_Title" name="Album.Title" type="text" value="" />
			  <span class="field-validation-error" data-valmsg-for="Album.Title" data-valmsg-replace="true">An Album Title is required</span>
		 </p>            
		 <p>
			  <label for="Album_Price">Price</label> 
			  <input class="input-validation-error" data-val="true" data-val-number="The field Price must be a number." data-val-range="Price must be between 0.01 and 100.00" data-val-range-max="100" data-val-range-min="0,01" data-val-required="Price is required" id="Album_Price" name="Album.Price" type="text" value="0" /> 
			  <span class="field-validation-error" data-valmsg-for="Album.Price" data-valmsg-replace="true">Price must be between 0.01 and 100.00</span>
		 </p>            
		 <p>
			  <label for="Album_AlbumArtUrl">Album Art URL</label>
			  <input data-val="true" data-val-length="The field Album Art URL must be a string with a maximum length of 1024." data-val-length-max="1024" id="Album_AlbumArtUrl" name="Album.AlbumArtUrl" type="text" value="" />
			  <span class="field-validation-valid" data-valmsg-for="Album.AlbumArtUrl" data-valmsg-replace="true"></span>
		 </p>          
	...
	
	````

	> **Note:** For each client validation rule, Unobtrusive jQuery adds an attribute with data-val-_rulename_="_message_". Below is a list of tags that Unobtrusive jQuery inserts into the html input field to perform client validation:
	>
	> - Data-val
	> - Data-val-number
	> - Data-val-range
	> - Data-val-range-min / Data-val-range-max
	> - Data-val-required
	> - Data-val-length
	> - Data-val-length-max / Data-val-length-min
	> 
	> All the data values are filled with model **Data Annotation**. Then, all the logic that works at server side can be run at client side.
	> For example, Price attribute has the following data annotation in the model: 
	>
	> ````C#
	> [Required(ErrorMessage = "Price is required")]
	> [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
	> public object Price { get; set; }
	> ````
	> After using Unobtrusive jQuery, the generated code is:
	>
	> ````HTML
	> <input data-val="true"
	> data-val-number="The field Price must be a number."
	> data-val-range="Price must be between 0.01 and 100.00"
	> data-val-range-max="100"
	> data-val-range-min="0.01"
	> data-val-required="Price is required"
	> id="Album_Price" name="Album.Price" type="text" value="0" />
	> ````  
	
---

<a name="Summary" />
## Summary ##

By completing this Hands-On Lab you have learned how to enable users to change the data stored in the database with the use of the following:

- Controller actions like Index, Create, Edit, Delete
- ASP.NET MVC's scaffolding feature for displaying properties in an HTML table
- Custom HTML helpers to improve user experience
- Action methods that react to either HTTP-GET or HTTP-POST calls
- A shared editor template for similar View templates like Create and Edit
- Form elements like drop-downs
- Data annotations for Model validation
- Client Side Validation using jQuery Unobtrusive library
