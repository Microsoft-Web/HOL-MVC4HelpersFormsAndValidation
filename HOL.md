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

- [Microsoft Visual Studio Express 2012 for Web](http://www.microsoft.com/visualstudio/eng/products/visual-studio-express-for-web) or superior (read [Appendix A](#AppendixA) for instructions on how to install it).

### Installing Code Snippets ###

For convenience, much of the code you will be managing along this lab is available as Visual Studio code snippets. To install the code snippets run **.\Source\Setup\CodeSnippets.vsi** file.

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

<a name="Ex1Task1" />
####Task 1 - Creating the StoreManagerController####
In this task, you will create a new controller called **StoreManagerController** to support CRUD operations.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex1-Begin.sln** solution located in the **Source\Ex1-CreatingTheStoreManagerController\Begin** folder of this lab.

1. In the Solution Explorer, click the **WebFormsLab** project and select **Manage NuGet Packages**.

1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

1. Build the solution by clicking **Build** | **Build Solution**.

1. Add a new controller. To do this, right-click the **Controllers** folder within the Solution Explorer, select **Add** and then the **Controller** command. Change the **Controller** **Name** to **StoreManagerController** and make sure the option **MVC controller with empty read/write actions** is selected. Click **Add**.

	 ![Add controller dialog](images/add-controller-dialog.png?raw=true "Add controller dialog")

	_Add Controller Dialog_

	A new Controller class is generated. Since you indicated to add actions for read/write, stub methods for those, common CRUD actions are created with TODO comments filled in, prompting to include the application specific logic.

<a name="Ex1Task2" />
#### Task 2 – Customizing the StoreManager Index####

In this task, you will customize the StoreManager Index action method to return a View with the list of albums from the database.

1.	In the StoreManagerController class, add the following  _using_ directives.

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 using MvcMusicStore_)

	<!-- mark:1,2 -->
	````C#
	using System.Data;
	using System.Data.Entity;
	using MvcMusicStore.Models;
	````

1. Add a field to the **StoreManagerController** to hold an instance of **MusicStoreEntities.**

	(Code Snippet – _ASP.NET MVC 4 Helpers and Forms and Validation – Ex1 MusicStoreEntities_)

	<!-- mark:3 -->
	````C#
	public class StoreManagerController : Controller
	{
		private MusicStoreEntities db = new MusicStoreEntities();
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
		var albums = this.db.Albums.Include(a => a.Genre).Include(a => a.Artist)
			 .OrderBy(a => a.Price);

		return this.View(albums.ToList());
	}

	````

<a name="Ex1Task3" />
#### **Task 3 - Creating the Index View**  ####

In this task, you will create the Index View template to display the list of albums returned by the **StoreManager** Controller.

1.	Before creating the new View template, you should build the project so that the **Add View Dialog** knows about the **Album** class to use. Select **Debug | Build MvcMusicSrore** to build the solution.
 
1.	Right-click inside the **Index** action method and select **Add View**. This will bring up the **Add View** dialog.

	![Add view](images/index-add-view.png?raw=true "Add view")
	
	_Adding a View from within the Index method_ 

1.	In the Add View dialog, verify that the View Name is **Index**. Select the **Create a strongly-typed view** option, and select **Album (MvcMusicStore.Models)** from the **Model class** drop-down. Select **List** from the **Scaffold template** drop-down. Leave the **View engine** to **Razor**  and the other fields with their default value and then click **Add**.
	
	![Adding an index view](images/index-add-view-dialog.png?raw=true "Adding an index view")
 
	_Adding an Index View_


<a name="Ex1Task4" />
#### Task 4 - Customizing the scaffold of the Index View####

In this task, you will adjust the simple View template created with ASP.NET MVC scaffolding feature to have it display the fields you want.

>**Note:** The **scaffolding** support within ASP.NET MVC generates a simple View template which lists all fields in the Album model. **Scaffolding** provides a quick way to get started on a strongly typed view: rather than having to write the View template manually, scaffolding quickly generates a default template and then you can modify the generated code.

1.	Review the code created. The generated list of fields will be part of the following HTML table that **Scaffolding** is using for displaying tabular data.

	````HTML
	@model IEnumerable<MvcMusicStore.Models.Album>

	@{
		 ViewBag.Title = "Index";
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

1.	Replace the **\<table\>** code with the following code to display only the **Genre**, **Artist**, **Album Title**, and **Price** fields. This deletes the **AlbumId** and **Album Art URL** columns. Also, it changes GenreId and ArtistId columns to display their linked class properties of **Artist.Name** and **Genre.Name**, and removes the **Details** link.

	<!-- mark:1-39 -->
	````HTML
	<table>
		<tr>
		  <th></th>
		  <th>Genre</th>
		  <th>Artist</th>
		  <th>Title</th>
		  <th>Price</th>
		</tr>

		@foreach (var item in Model) {
		 <tr>
			  <td>
					@Html.ActionLink("Edit", "Edit", new { id=item.AlbumId }) |
				
					@Html.ActionLink("Delete", "Delete", new { id=item.AlbumId })
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Genre.Name)
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Artist.Name)
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Title)
			  </td>
			  <td>
					@Html.DisplayFor(modelItem => item.Price)
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
	}
	
	<h2>Albums</h2>
	````


<a name="Ex1Task5" />
#### Task 5 - Running the Application####
In this task, you will test that the **StoreManager** **Index** View template displays a list of albums according to the design of the previous steps.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager** to verify that a list of albums is displayed, showing their **Title**, **Artist** and **Genre**.

	![Browsing the list of albums](images/browsing-the-list-of-albums.png?raw=true "Browsing the list of albums")

	_Browsing the list of albums_

<a name="Exercise2" />
### Exercise 2: Adding an HTML Helper###

The StoreManager Index page has one potential issue: Title and Artist Name properties can both be long enough to throw off the table formatting. In this exercise you will learn how to add a custom HTML helper to truncate that text.

In the following figure, you can see how the format is modified because of the length of the text.

![Browsing the list of Albums with not truncated text](images/browsing-the-list-of-albums-with-not-truncate.png?raw=true "Browsing the list of Albums with not truncated text")

_Browsing the list of Albums with not truncated text_


<a name="Ex2Task1" />
#### Task 1 - Extending the HTML Helper####

In this task, you will add a new method **Truncate** to the **HTML** object exposed within ASP.NET MVC Views. To do this, you will implement an **extension method** to the built-in **System.Web.Mvc.HtmlHelper** class provided by ASP.NET MVC.

>**Note:** To read more about **Extension Methods**, please visit this msdn article. <http://msdn.microsoft.com/en-us/library/bb383977.aspx>.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex2-Begin.sln** solution located in the **Source\Ex2-AddingAnHTMLHelper\Begin** folder of this lab. Alternatively, you can continue working on your existing solution from the previous exercise.

	1. If you opened the provided **Begin** solution, you will need to download some missing NuGet packages before continue. To do this, in the Solution Explorer, click the **WebFormsLab** project **Manage NuGet Packages**.

	1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

	1. Finally, build the solution by clicking **Build** | **Build Solution**.

1. Open StoreManager's Index View. To do this, in the Solution Explorer expand the **Views** folder, then the **StoreManager** and open the **Index.cshtml** file.

1.	Add the following code below the **@model** directive to define the **Truncate** helper method.

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex2 TruncateHelper_)

	<!-- mark:3-10 -->
	````C#
	@model IEnumerable<MvcMusicStore.Models.Album>

	@helper Truncate(string input, int length)
	{
		 if (input.Length <= length) {
			  @input
		 } else {
			  @input.Substring(0, length)<text>...</text>
		 }
	} 

	@{
		 ViewBag.Title = "Store Manager - All Albums";
	}

	<h2>Albums</h2>
	````

<a name="Ex2Task2" />
#### Task 2 - Truncating Text in the Page####

In this task, you will use the **Truncate** method to truncate the text in the View template.

1. Open StoreManager's Index View. To do this, in the Solution Explorer expand the **Views** folder, then the **StoreManager** and open the **Index.cshtml** file.
1. Replace the lines that show the **Artist Name** and Album's **Title**. To do this, replace the following lines.

	<!-- mark:11,14 -->
	````HTML
	<tr>
		 <td>
			  @Html.ActionLink("Edit", "Edit", new { id=item.AlbumId }) |

			  @Html.ActionLink("Delete", "Delete", new { id=item.AlbumId })
		 </td>
		 <td>
			  @Html.DisplayFor(modelItem => item.Genre.Name)
		 </td>
		 <td>
			  @Truncate(item.Artist.Name, 25)
		 </td>
		 <td>
			  @Truncate(item.Title, 25)
		 </td>
		 <td>
			  @Html.DisplayFor(modelItem => item.Price)
		 </td>
	</tr>
	````
	
<a name="Ex2Task3" />
#### Task 3 - Running the Application####

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


<a name="Ex3Task1" />
#### Task 1 - Implementing the HTTP-GET Edit Action Method ####

In this task, you will implement the HTTP-GET version of the Edit action method to retrieve the appropriate Album from the database, as well as a list of all Genres and Artists. It will package this data up into the **StoreManagerViewModel** object defined in the last step, which will then be passed to a View template to render the response with.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex3-Begin.sln** solution located in the **Source\Ex3-CreatingTheEditView\Begin** folder of this lab. Alternatively, you can continue working on your existing solution from the previous exercise.

	1. If you opened the provided **Begin** solution, you will need to download some missing NuGet packages before continue. To do this, in the Solution Explorer, click the **WebFormsLab** project **Manage NuGet Packages**.

	1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

	1. Finally, build the solution by clicking **Build** | **Build Solution**.

1.	Open the **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.

1.	Replace the **HTTP-GET Edit** action method with the following code to retrieve the appropriate **Album** as well as the **Genres** and **Artists** lists.

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex3 StoreManagerController HTTP-GET Edit action_)

	<!-- mark:3-13 -->
	````C#
	public ActionResult Edit(int id)
	{
		Album album = this.db.Albums.Find(id);

		if (album == null)
		{
			 return this.HttpNotFound();
		}

		this.ViewBag.GenreId = new SelectList(this.db.Genres, "GenreId", "Name", album.GenreId);
		this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name", album.ArtistId);

		return this.View(album);
	}
	````

	>**Note:** You are using **System.Web.Mvc** **SelectList** for Artists and Genres instead of the **System.Collections.Generic** List.
	>
	>**SelectList** is a cleaner way to populate HTML dropdowns and manage things like current selection. Instantiating and later setting up these ViewModel objects in the controller action will make the Edit form scenario cleaner.

<a name="Ex3Task2" />
#### Task 2 - Creating the Edit View####

In this task, you will create an Edit View template that will later display the album properties.

1. Create the Edit View. To do this, right-click inside the **Edit** action method and select **Add View**.
1.	In the Add View dialog, verify that the View Name is **Edit**. Check the **Create a strongly-typed view** checkbox and select **Album (MvcMusicStore.Models)** from the **View data class** drop-down. Select **Edit** from the **Scaffold template** drop-down. Leave the other fields with their default value and then click **Add**.

	![Adding an Edit view](images/adding-an-edit-view.png?raw=true "Adding an Edit view")

	_Adding an Edit view_

<a name="Ex3Task3" />
#### Task 3 - Running the Application ####

In this task, you will test that the **StoreManager** **Edit** View page displays the properties' values for the album passed as parameter.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager/Edit/136** to verify that the properties' values for the album passed are displayed.

	![Browsing Album's Edit View](images/browsing-albums-edit-view.png?raw=true "Browsing Album's Edit View")

	_Browsing Album's Edit view_

<a name="Ex3Task4" />
#### Task 4 - Implementing drop-downs on the Album Editor Template ####

In this task, you will add drop-downs to the View template created in the last task, so that the user can select from a list of Artists and Genres.

1.	Replace all the **Album** fieldset code with the following:

	````HTML
	<fieldset>
		 <legend>Album</legend>

		 @Html.HiddenFor(model => model.AlbumId)

		 <div class="editor-label">
			  @Html.LabelFor(model => model.Title)
		 </div>
		 <div class="editor-field">
			  @Html.EditorFor(model => model.Title)
			  @Html.ValidationMessageFor(model => model.Title)
		 </div>

		 <div class="editor-label">
			  @Html.LabelFor(model => model.Price)
		 </div>
		 <div class="editor-field">
			  @Html.EditorFor(model => model.Price)
			  @Html.ValidationMessageFor(model => model.Price)
		 </div>

		 <div class="editor-label">
			  @Html.LabelFor(model => model.AlbumArtUrl)
		 </div>
		 <div class="editor-field">
			  @Html.EditorFor(model => model.AlbumArtUrl)
			  @Html.ValidationMessageFor(model => model.AlbumArtUrl)
		 </div>
			  
		 <div class="editor-label">
			  @Html.LabelFor(model => model.Artist)
		 </div>
		 <div class="editor-field">
			  @Html.DropDownList("ArtistId", (SelectList) ViewData["Artists"])
			  @Html.ValidationMessageFor(model => model.ArtistId)
		 </div>

		 <div class="editor-label">
			  @Html.LabelFor(model => model.Genre)
		 </div>
		 <div class="editor-field">
			  @Html.DropDownList("GenreId", (SelectList) ViewData["Genres"])
			  @Html.ValidationMessageFor(model => model.GenreId)
		 </div>

		 <p>
			  <input type="submit" value="Save" />
		 </p>
	</fieldset>          
	````

	>**Note:** An **Html.DropDownList** helper has been added to render drop-downs for choosing Artists and Genres. The parameters passed to **Html.DropDownList** are:
	>
	>1. The name of the form field (**"ArtistId"**).
	>1. The **SelectList** of values for the drop-down.

<a name="Ex3Task5" />
#### Task 5 - Running the Application ####

In this task, you will test that the **StoreManager** **Edit** View page displays drop-downs instead of Artist and Genre ID text fields.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager/Edit/388** to verify that it displays drop-downs instead of Artist and Genre ID text fields.

	![Browsing Album's Edit View with drop downs](images/browsing-albums-edit-view-with-drop-downs.png?raw=true "Browsing Album's Edit View with drop downs")

	_Browsing Album’s Edit view, this time with dropdowns_


<a name="Ex3Task6" />
#### Task 6 - Implementing the HTTP-POST Edit action method ####

Now that the Edit View displays as expected, you need to implement the HTTP-POST Edit Action method to save the changes made to the Album.

1.	Close the browser if needed, to return to the Visual Studio window. Open **StoreManagerController** from the **Controllers** folder.
1.	Replace **HTTP-POST Edit** action method code with the following (note that the method that must be replaced is overloaded version that receives two parameters):

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex3 StoreManagerController HTTP-POST Edit action_)
	
	<!-- mark:2-16 -->
	````C#
	[HttpPost]
	public ActionResult Edit(Album album)
	{
		 if (ModelState.IsValid)
		 {
			  this.db.Entry(album).State = EntityState.Modified;
			  this.db.SaveChanges();

			  return this.RedirectToAction("Index");
		 }

		 this.ViewBag.GenreId = new SelectList(this.db.Genres, "GenreId", "Name", album.GenreId);
		 this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name", album.ArtistId);

		 return this.View(album);
	}
	````

	>**Note:** This method will be executed when the user clicks the **Save** button of the View and performs an HTTP-POST of the form values back to the server to persist them in the database. The decorator **[HttpPost]** indicates that the method should be used for those HTTP-POST scenarios.
	> The method takes an **Album** object. ASP.NET MVC will automatically create the Album object from the posted \<form\> values.
	>
	> The method will perform these steps:
	>
	> 1. If model is valid:
	>
	>	1. Update the album entry in the context to mark it as a modified object.
	>
	>	1. Save the changes and redirect to the index view.
	>
	> 1. If the model is not valid, it will populate the ViewBag with the **GenreId** and **ArtistId**, then it will return the view with the received Album object to allow the user perform any required update.


<a name="Ex3Task7" />
#### Task 7 - Running the Application ####

In this task, you will test that the **StoreManager Edit** View page actually saves the updated Album data in the database.

1.	Press **F5** to run the Application.

1.	The project starts in the Home page. Change the URL to **/StoreManager/Edit/136**. Change the Album title to **Greatest Hits Vol. 1** and click on **Save**. Verify that album’s title actually changed in the list of albums.
	
	![Updating an album](images/updating-an-album.png?raw=true "Updating an album")

	_Updating an Album_

<a name="Exercise4" />
### Exercise 4: Adding a Create View###

Now that the **StoreManagerController** supports the **Edit** ability, in this exercise you will learn how to add a Create View template to let store managers add new Albums to the application.

Like you did with the Edit functionality, you will implement the Create scenario using two separate methods within the **StoreManagerController** class:

1. One action method will display an empty form when store managers first visit the **/StoreManager/Create** URL. 

1.	A second action method will handle the scenario where the store manager clicks the **Save** button within the form and submits the values back to the **/StoreManager/Create** URL as an HTTP-POST.

	
<a name="Ex4Task1" />
#### Task 1 - Implementing the HTTP-GET Create action method ####

In this task, you will implement the HTTP-GET version of the Create action method to retrieve a list of all Genres and Artists, package this data up into a **StoreManagerViewModel** object, which will then be passed to a View template.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex4-Begin.sln** solution located in the **Source\Ex4-AddingACreateView\Begin** folder of this lab. Alternatively, you can continue working on your existing solution from the previous exercise.

	1. If you opened the provided **Begin** solution, you will need to download some missing NuGet packages before continue. To do this, in the Solution Explorer, click the **WebFormsLab** project **Manage NuGet Packages**.

	1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

	1. Finally, build the solution by clicking **Build** | **Build Solution**.

1.	Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.

1.	Replace the **Create** action method code with the following:

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex4 StoreManagerController HTTP-GET Create action_)

	<!-- mark:4-10 -->
	````C#	
	//
	// GET: /StoreManager/Create

	public ActionResult Create()
	{
		 this.ViewBag.GenreId = new SelectList(this.db.Genres, "GenreId", "Name");
		 this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name");

		 return this.View();
	}
	````

<a name="Ex4Task2" />
#### Task 2 - Adding the Create View ####

In this task, you will add the Create View template that will display a new (empty) Album form.

1.	Right-click inside the **Create** action method and select **Add View**. This will bring up the Add View dialog.
1.	In the Add View dialog, verify that the View Name is **Create**. Select the **Create a strongly-typed view** option and select **Album (MvcMusicStore.Models)** from the **Model class** drop-down and **Create** from the **Scaffold template** drop-down. Leave the other fields with their default value and then click **Add**. 


	![Adding a create view](images/adding-a-create-view.png?raw=true "adding-a-create-view.png")

	_Adding the Create View_


1.	Update the **GenreId** and **ArtistId** fields to use a drop-down list as shown below:

	<!-- mark:5-19 -->
	````HTML
	...
	<fieldset>
		 <legend>Album</legend>
			  
		 <div class="editor-label">
			  @Html.LabelFor(model => model.GenreId, "Genre")
		 </div>
		 <div class="editor-field">
			  @Html.DropDownList("GenreId", String.Empty)
			  @Html.ValidationMessageFor(model => model.GenreId)
		 </div>

		 <div class="editor-label">
			  @Html.LabelFor(model => model.ArtistId, "Artist")
		 </div>
		 <div class="editor-field">
			  @Html.DropDownList("ArtistId", String.Empty)
			  @Html.ValidationMessageFor(model => model.ArtistId)
		 </div>

		 <div class="editor-label">
			  @Html.LabelFor(model => model.Title)
		 </div>
	    ...
	````

<a name="Ex4Task3" />
#### Task 3 - Running the Application ####

In this task, you will test that the **StoreManager** **Create** View page displays an empty Album form.

1. Press **F5** to run the Application.
1. The project starts in the Home page. Change the URL to **/StoreManager/Create**. Verify that an empty form is displayed for filling the new Album properties.
	
	![Create View with an empty form](images/create-view-empty-form.png?raw=true "Create View with an empty form")

	_Create View with an empty form_


<a name="Ex4Task4" />
#### Task 4 - Implementing the HTTP-POST Create Action Method ####

In this task, you will implement the HTTP-POST version of the Create action method that will be invoked when a user clicks the **Save** button. The method should save the new album in the database.

1.	Close the browser if needed, to return to the Visual Studio window. Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.
1.	Replace **HTTP-POST Create** action method code with the following:

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex4 StoreManagerController HTTP- POST Create action_)

	<!-- mark:2-16 -->
	````C#
	[HttpPost]
	public ActionResult Create(Album album)
	{
		 if (ModelState.IsValid)
		 {
			  this.db.Albums.Add(album);
			  this.db.SaveChanges();

			  return this.RedirectToAction("Index");
		 }

		 this.ViewBag.GenreId = new SelectList(this.db.Genres, "GenreId", "Name", album.GenreId);
		 this.ViewBag.ArtistId = new SelectList(this.db.Artists, "ArtistId", "Name", album.ArtistId);

		 return this.View(album);
	}
	````

	>**Note:** The Create action is pretty similar to the previous Edit action method but instead of setting the object as modified, it is being added to the context.

<a name="Ex4Task5" />
#### Task 5 - Running the Application ####

In this task, you will test that the **StoreManager Create** View page lets you create a new Album and then redirects to the StoreManager Index View.

1. Press **F5** to run the Application.
1. The project starts in the Home page. Change the URL to **/StoreManager/Create**. Fill all the form fields with data for a new Album, like the one in the following figure:

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
	
<a name="Ex5Task1" />
#### Task 1 - Implementing the HTTP-GET Delete Action Method ####

In this task, you will implement the HTTP-GET version of the Delete action method to retrieve the album’s information.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex5-Begin.sln** solution located in the **Source\Ex5-HandlingDeletion\Begin** folder of this lab. Alternatively, you can continue working on your existing solution from the previous exercise.

	1. If you opened the provided **Begin** solution, you will need to download some missing NuGet packages before continue. To do this, in the Solution Explorer, click the **WebFormsLab** project **Manage NuGet Packages**.

	1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

	1. Finally, build the solution by clicking **Build** | **Build Solution**.

1. Open **StoreManagerController** class. To do this, expand the **Controllers** folder and double-click **StoreManagerController.cs**.

1. The Delete controller action is exactly the same as the previous Store Details controller action: it queries the **album** object from the database using the **id** provided in the URL and returns the appropriate **View**. To do this, replace the HTTP-GET **Delete** action method code with the following:


	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex5 Handling Deletion HTTP-GET Delete action_)

	<!-- mark:4-14 -->
	````C#
	//
	// GET: /StoreManager/Delete/5

	public ActionResult Delete(int id)
	{
		 Album album = this.db.Albums.Find(id);

		 if (album == null)
		 {
			  return this.HttpNotFound();
		 }

		 return this.View(album);
	}
	````

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
		 Album album = this.db.Albums.Find(id);
		 this.db.Albums.Remove(album);
		 this.db.SaveChanges();

		 return RedirectToAction("Index");	
	}
	````

<a name="Ex5Task4" />
#### Task 4 - Running the Application ####

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

For a simple Model class, adding a Data Annotation is just handled by adding a **using** statement for **System.ComponentModel.DataAnnotation**, then placing a **[Required]** attribute on the appropriate properties.
The following example would make the **Name** property a required field in the View.

````C#
using System.ComponentModel.DataAnnotations;
namespace SuperheroSample.Models
{
    public class Superhero
    {
        [Required]
        public string Name { get; set; }
        public bool WearsCape { get; set; }
    }
}
````

This is a little more complex in cases like this application where the Entity Data Model is generated. If you added Data Annotations directly to the model classes, they would be overwritten if you update the model from the database.
Instead, you can make use of metadata partial classes which will exist to hold the annotations and are associated with the model classes using the **\[MetadataType\]** attribute.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex6-Begin.sln** solution located in the **Source\Ex6-AddingValidation\Begin** folder of this lab. Alternatively, you can continue working on your existing solution from the previous exercise.

	1. If you opened the provided **Begin** solution, you will need to download some missing NuGet packages before continue. To do this, in the Solution Explorer, click the **WebFormsLab** project **Manage NuGet Packages**.

	1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

	1. Finally, build the solution by clicking **Build** | **Build Solution**.

1. Open the **Album.cs** from the **Models** folder.

1. Replace **Album.cs** content with the highlighted code, so that it looks like the following:

	>**Note:** The line **\[DisplayFormat(ConvertEmptyStringToNull=false)\]** indicates that empty strings from the model won’t be converted to null when the data field is updated in the data source. 
	>This setting will avoid an exception when the Entity Framework assigns null values to the model before Data Annotation validates the fields.

	(Code Snippet - _ASP.NET MVC 4 Helpers and Forms and Validation - Ex6 Album metadata partial class_)

	<!-- mark:5-42 -->
	````C#
	namespace MvcMusicStore.Models
	{
		 using System.ComponentModel;
		 using System.ComponentModel.DataAnnotations;

		 public class Album
		 {
			  [ScaffoldColumn(false)]
			  public int AlbumId { get; set; }

			  [DisplayName("Genre")]
			  public int GenreId { get; set; }
			  
			  [DisplayName("Artist")]
			  public int ArtistId { get; set; }

			  [Required(ErrorMessage = "An Album Title is required")]
			  [DisplayFormat(ConvertEmptyStringToNull = false)]
			  [StringLength(160, MinimumLength = 2)]
			  public string Title { get; set; }

			  [Required(ErrorMessage = "Price is required")]
			  [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
			  [DataType(DataType.Currency)]
			  public decimal Price { get; set; }

			  [DisplayName("Album Art URL")]
			  [DataType(DataType.ImageUrl)]
			  [StringLength(1024)]
			  public string AlbumArtUrl { get; set; }

			  public virtual Genre Genre { get; set; }

			  public virtual Artist Artist { get; set; }
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
	>-  ScaffoldColumn - Allows hiding fields from editor forms

<a name="Ex06Task2" />
#### Task 2 - Running the Application ####

In this task, you will test that the Create and Edit pages validate fields, using the display names chosen in the last task.

1.	Press **F5** to run the Application.
1.	The project starts in the Home page. Change the URL to **/StoreManager/Create**.  Verify that the display names match the ones in the partial class (like **Album Art URL** instead of **AlbumArtUrl**)
1.	Click **Save**, without filling the form. Verify that you get the corresponding validation messages.

	![Validated fields in the Create page](images/validated-fields-in-create-page.png?raw=true "Validated fields in the Create page")

	_Validated fields in the Create page_

1.	You can verify that the same occurs with the **Edit** page. Change the URL to **/StoreManager/Edit/136** and verify that the display names match the ones in the partial class (like **Album Art URL** instead of **AlbumArtUrl**). Empty the **Title** and **Price** fields and click **Save**. Verify that you get the corresponding validation messages.

	![Validated fields in the Edit page](images/validated-fields-in-edit-page.png?raw=true)

	_Validated fields in the Edit page_

<a name="Exercise7" /> 
### Exercise 7: Using Unobtrusive jQuery at Client Side ###

In this exercise, you will learn how to enable MVC 4 Unobtrusive jQuery validation at client side.

> **Note:** The Unobtrusive jQuery uses data-ajax prefix JavaScript to invoke action methods on the server rather than intrusively emitting inline client scripts.

<a name="Ex7Task1" />
#### Task 1 - Running the Application before Enabling Unobtrusive jQuery ####

In this task, you will run the application before including jQuery in order to compare both validation models.

1. Open **Visual Studio 2012** and open the **FormsLab-Ex7-Begin.sln** solution located in the **Source\Ex7-UnobtrusivejQueryValidation\Begin** folder of this lab. Alternatively, you can continue working on your existing solution from the previous exercise.

	1. If you opened the provided **Begin** solution, you will need to download some missing NuGet packages before continue. To do this, in the Solution Explorer, click the **WebFormsLab** project **Manage NuGet Packages**.

	1. In the **Manage NuGet Packages** page, click **Restore** in order to download missing packages.

	1. Finally, build the solution by clicking **Build** | **Build Solution**.

1. Press **F5** to run the application.

1. The project starts in the Home page. Browse **/StoreManager/Create** and click **Save** without filling the form to verify that you get validation messages:

 	![Client validation disabled](./images/validated-fields-in-create-page.png?raw=true "Client validation disabled")
 
	_Client validation disabled_

1. In the browser, open the HTML source code:

	<!-- mark:7,15 -->
	````HTML
	...
			  <div class="editor-label">
					<label for="Price">Price</label>
			  </div>
			  <div class="editor-field">
					<input class="text-box single-line" id="Price" name="Price" type="text" value="" />
					<span class="field-validation-valid" id="Price_validationMessage"></span>
			  </div>

			  <div class="editor-label">
					<label for="AlbumArtUrl">Album Art URL</label>
			  </div>
			  <div class="editor-field">
					<input class="text-box single-line" id="AlbumArtUrl" name="AlbumArtUrl" type="text" value="" />
					<span class="field-validation-valid" id="AlbumArtUrl_validationMessage"></span>
			  </div>

			  <p>
					<input type="submit" value="Create" />
			  </p>
		 </fieldset>
	</form><script type="text/javascript">
	//<![CDATA[
	if (!window.mvcClientValidationMetadata) { window.mvcClientValidationMetadata = []; }
	window.mvcClientValidationMetadata.push({"Fields":[{"FieldName":"GenreId","ReplaceValidationMessageContents":true,"ValidationMessageId":"GenreId_validationMessage","ValidationRules":[{"ErrorMessage":"The Genre field is required.","ValidationParameters":{},"ValidationType":"required"},{"ErrorMessage":"The field Genre must be a number.","ValidationParameters":{},"ValidationType":"number"}]},{"FieldName":"ArtistId","ReplaceValidationMessageContents":true,"ValidationMessageId":"ArtistId_validationMessage","ValidationRules":[{"ErrorMessage":"The Artist field is required.","ValidationParameters":{},"ValidationType":"required"},{"ErrorMessage":"The field Artist must be a number.","ValidationParameters":{},"ValidationType":"number"}]},{"FieldName":"Title","ReplaceValidationMessageContents":true,"ValidationMessageId":"Title_validationMessage","ValidationRules":[{"ErrorMessage":"An Album Title is required","ValidationParameters":{},"ValidationType":"required"},{"ErrorMessage":"The field Title must be a string with a minimum length of 2 and a maximum length of 160.","ValidationParameters":{"min":2,"max":160},"ValidationType":"length"}]},{"FieldName":"Price","ReplaceValidationMessageContents":true,"ValidationMessageId":"Price_validationMessage","ValidationRules":[{"ErrorMessage":"Price must be between 0.01 and 100.00","ValidationParameters":{"min":0.01,"max":100},"ValidationType":"range"},{"ErrorMessage":"Price is required","ValidationParameters":{},"ValidationType":"required"},{"ErrorMessage":"The field Price must be a number.","ValidationParameters":{},"ValidationType":"number"}]},{"FieldName":"AlbumArtUrl","ReplaceValidationMessageContents":true,"ValidationMessageId":"AlbumArtUrl_validationMessage","ValidationRules":[{"ErrorMessage":"The field Album Art URL must be a string with a maximum length of 1024.","ValidationParameters":{"max":1024},"ValidationType":"length"}]}],"FormId":"form0","ReplaceValidationSummary":false,"ValidationSummaryId":"validationSummary"});
	//]]>
	</script>
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
		 <add key="webpages:Enabled" value="false" />
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

1. Make sure the following script files, **jquery.validate** and  **jquery.validate.unobtrusive**, are referenced in the view throught the "**~/bundles/jqueryval**" bundle.

	````CSHTML
	...
	@section Scripts {
		 @Scripts.Render("~/bundles/jqueryval")
	}
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

	<!-- mark:6-7,14-15,22-23 -->
	````HTML
	...
	<div class="editor-label">
		<label for="Title">Title</label>
	</div>
	<div class="editor-field">
		<input class="text-box single-line" data-val="true" data-val-length="The field Title must be a string with a minimum length of 2 and a maximum length of 160." data-val-length-max="160" data-val-length-min="2" data-val-required="An Album Title is required" id="Title" name="Title" type="text" value="" />
		<span class="field-validation-valid" data-valmsg-for="Title" data-valmsg-replace="true"></span>
	</div>

	<div class="editor-label">
		<label for="Price">Price</label>
	</div>
	<div class="editor-field">
		<input class="text-box single-line" data-val="true" data-val-number="The field Price must be a number." data-val-range="Price must be between 0.01 and 100.00" data-val-range-max="100" data-val-range-min="0.01" data-val-required="Price is required" id="Price" name="Price" type="text" value="" />
		<span class="field-validation-valid" data-valmsg-for="Price" data-valmsg-replace="true"></span>
	</div>

	<div class="editor-label">
		<label for="AlbumArtUrl">Album Art URL</label>
	</div>
	<div class="editor-field">
		<input class="text-box single-line" data-val="true" data-val-length="The field Album Art URL must be a string with a maximum length of 1024." data-val-length-max="1024" id="AlbumArtUrl" name="AlbumArtUrl" type="text" value="" />
		<span class="field-validation-valid" data-valmsg-for="AlbumArtUrl" data-valmsg-replace="true"></span>
	</div>        
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


<a name="AppendixA" />
## Appendix A: Installing Visual Studio Express 2012 for Web ##

You can install **Microsoft Visual Studio Express 2012 for Web** or another "Express" version using the **[Microsoft Web Platform Installer](http://www.microsoft.com/web/downloads/platform.aspx)**. The following instructions guide you through the steps required to install _Visual studio Express 2012 for Web_ using _Microsoft Web Platform Installer_.

1. Go to [http://go.microsoft.com/?linkid=9810169](http://go.microsoft.com/?linkid=9810169). Alternatively, if you already have installed Web Platform Installer, you can open it and search for the product "_Visual Studio Express 2012 for Web with Windows Azure SDK_".

1. Click on **Install Now**. If you do not have **Web Platform Installer** you will be redirected to download and install it first.

1. Once **Web Platform Installer** is open, click **Install** to start the setup.

	![Install Visual Studio Express](images/install-visual-studio-express.png?raw=true "Install Visual Studio Express")

 	_Install Visual Studio Express_

1. Read all the products' licenses and terms and click **I Accept** to continue.

	![Accepting the license terms](images/accepting-the-license-terms.png?raw=true)

	_Accepting the license terms_

1. Wait until the downloading and installation process completes.

	![Installation progress](images/installation-progress.png?raw=true)

	_Installation progress_

1. When the installation completes, click **Finish**.

	![Installation completed](images/installation-completed.png?raw=true)

	_Installation completed_

1. Click **Exit** to close Web Platform Installer.

1. To open Visual Studio Express for Web, go to the **Start** screen and start writing "**VS Express**", then click on the **VS Express for Web** tile.

	![VS Express for Web tile](images/vs-express-for-web-tile.png?raw=true)

	_VS Express for Web tile_

<a name="AppendixB" />
## Appendix B: Publishing an ASP.NET MVC 4 Application using Web Deploy ##

This appendix will show you how to create a new web site from the Windows Azure Management Portal and publish the application you obtained by following the lab, taking advantage of the Web Deploy publishing feature provided by Windows Azure.

<a name="ApxBTask1"></a>
#### Task 1 – Creating a New Web Site from the Windows Azure Portal ####

1. Go to the [Windows Azure Management Portal](https://manage.windowsazure.com/) and sign in using the Microsoft credentials associated with your subscription.

	![Log on to Windows Azure portal](images/login.png?raw=true "Log on to Windows Azure portal")

	_Log on to Windows Azure Management Portal_

1. Click **New** on the command bar.

	![Creating a new Web Site](images/new-website.png?raw=true "Creating a new Web Site")

	_Creating a new Web Site_

1. Click **Compute** | **Web Site**. Then select **Quick Create** option. Provide an available URL for the new web site and click **Create Web Site**.

	> **Note:** A Windows Azure Web Site is the host for a web application running in the cloud that you can control and manage. The Quick Create option allows you to deploy a completed web application to the Windows Azure Web Site from outside the portal. It does not include steps for setting up a database.

	![Creating a new Web Site using Quick Create](images/quick-create.png?raw=true "Creating a new Web Site using Quick Create")

	_Creating a new Web Site using Quick Create_

1. Wait until the new **Web Site** is created.

1. Once the Web Site is created click the link under the **URL** column. Check that the new Web Site is working.

	![Browsing to the new web site](images/navigate-website.png?raw=true "Browsing to the new web site")

	_Browsing to the new web site_

	![Web site running](images/website-working.png?raw=true "Web site running")

	_Web site running_

1. Go back to the portal and click the name of the web site under the **Name** column to display the management pages.

	![Opening the web site management pages](images/go-to-the-dashboard.png?raw=true "Opening the web site management pages")
	
	_Opening the Web Site management pages_

1. In the **Dashboard** page, under the **quick glance** section, click the **Download publish profile** link.

	> **Note:** The _publish profile_ contains all of the information required to publish a web application to a Windows Azure website for each enabled publication method. The publish profile contains the URLs, user credentials and database strings required to connect to and authenticate against each of the endpoints for which a publication method is enabled. **Microsoft WebMatrix 2**, **Microsoft Visual Studio Express for Web** and **Microsoft Visual Studio 2012** support reading publish profiles to automate configuration of these programs for publishing web applications to Windows Azure websites. 

	![Downloading the web site publish profile](images/download-publish-profile.png?raw=true "Downloading the web site publish profile")
	
	_Downloading the Web Site publish profile_

1. Download the publish profile file to a known location. Further in this exercise you will see how to use this file to publish a web application to a Windows Azure Web Sites from Visual Studio.

	![Saving the publish profile file](images/save-link.png?raw=true "Saving the publish profile")
	
	_Saving the publish profile file_

<a name="ApxBTask2"></a>
#### Task 2 – Configuring the Database Server ####

If your application makes use of SQL Server databases you will need to create a SQL Database server. If you want to deploy a simple application that does not use SQL Server you might skip this task.

1. You will need a SQL Database server for storing the application database. You can view the SQL Database servers from your subscription in the Windows Azure Management portal at **Sql Databases** | **Servers** | **Server's Dashboard**. If you do not have a server created, you can create one using the **Add** button on the command bar. Take note of the **server name and URL, administrator login name and password**, as you will use them in the next tasks. Do not create the database yet, as it will be created in a later stage.

	![SQL Database Server Dashboard](images/sql-database-server-dashboard.png?raw=true "SQL Database Server Dashboard")

	_SQL Database Server Dashboard_

1. In the next task you will test the database connection from Visual Studio, for that reason you need to include your local IP address in the server's list of **Allowed IP Addresses**. To do that, click **Configure**, select the IP address from **Current Client IP Address** and paste it on the **Start IP Address** and **End IP Address** text boxes and click the ![add-client-ip-address-ok-button](images/add-client-ip-address-ok-button.png?raw=true) button.

	![Adding Client IP Address](images/add-client-ip-address.png?raw=true)

	_Adding Client IP Address_

1. Once the **Client IP Address** is added to the allowed IP addresses list, click on **Save** to confirm the changes.

	![Confirm Changes](images/add-client-ip-address-confirm.png?raw=true)

	_Confirm Changes_

<a name="ApxBTask3"></a>
#### Task 3 – Publishing an ASP.NET MVC 4 Application using Web Deploy ####

1. Go back to the ASP.NET MVC 4 solution. In the **Solution Explorer**,  right-click the web site project and select **Publish**.

	![Publishing the Application](images/publishing-the-application.png?raw=true "Publishing the Application")

	_Publishing the web site_

1. Import the publish profile you saved in the first task.

	![Importing the publish profile](images/importing-the-publish-profile.png?raw=true "Importing the publish profile")

	_Importing publish profile_

1. Click **Validate Connection**. Once Validation is complete click **Next**.

	> **Note:** Validation is complete once you see a green checkmark appear next to the Validate Connection button.

	![Validating connection](images/validating-connection.png?raw=true "Validating connection")

	_Validating connection_

1. In the **Settings** page, under the **Databases** section, click the button next to your database connection's textbox (i.e. **DefaultConnection**).

	![Web deploy configuration](images/web-deploy-configuration.png?raw=true "Web deploy configuration")

	_Web deploy configuration_

1. Configure the database connection as follows:
	* In the **Server name** type your SQL Database server URL using the _tcp:_ prefix.
	* In **User name** type your server administrator login name.
	* In **Password** type your server administrator login password.
	* Type a new database name.

	![Configuring destination connection string](images/configuring-destination-connection-string.png?raw=true "Configuring destination connection string")

	_Configuring destination connection string_

1. Then click **OK**. When prompted to create the database click **Yes**.

	![Creating the database](images/creating-the-database.png?raw=true "Creating the database string")

	_Creating the database_

1. The connection string you will use to connect to SQL Database in Windows Azure is shown within Default Connection textbox. Then click **Next**.

	![Connection string pointing to SQL Database](images/sql-database-connection-string.png?raw=true "Connection string pointing to SQL Database")

	_Connection string pointing to SQL Database_

1. In the **Preview** page, click **Publish**.

	![Publishing the web application](images/publishing-the-web-application.png?raw=true "Publishing the web application")

	_Publishing the web application_

1. Once the publishing process finishes, your default browser will open the published web site.


<a name="AppendixC"></a>
## Appendix C: Using Code Snippets ##

With code snippets, you have all the code you need at your fingertips. The lab document will tell you exactly when you can use them, as shown in the following figure.

 ![Using Visual Studio code snippets to insert code into your project](./images/Using-Visual-Studio-code-snippets-to-insert-code-into-your-project.png?raw=true "Using Visual Studio code snippets to insert code into your project")
 
_Using Visual Studio code snippets to insert code into your project_

_**To add a code snippet using the keyboard (C# only)**_

1. Place the cursor where you would like to insert the code.

1. Start typing the snippet name (without spaces or hyphens).

1. Watch as IntelliSense displays matching snippets' names.

1. Select the correct snippet (or keep typing until the entire snippet's name is selected).

1. Press the Tab key twice to insert the snippet at the cursor location.

 
   ![Start typing the snippet name](./images/Start-typing-the-snippet-name.png?raw=true "Start typing the snippet name")
 
_Start typing the snippet name_

   ![Press Tab to select the highlighted snippet](./images/Press-Tab-to-select-the-highlighted-snippet.png?raw=true "Press Tab to select the highlighted snippet")
 
_Press Tab to select the highlighted snippet_

   ![Press Tab again and the snippet will expand](./images/Press-Tab-again-and-the-snippet-will-expand.png?raw=true "Press Tab again and the snippet will expand")
 
_Press Tab again and the snippet will expand_

_**To add a code snippet using the mouse (C#, Visual Basic and XML)**_
1. Right-click where you want to insert the code snippet.

1. Select **Insert Snippet** followed by **My Code Snippets**.

1. Pick the relevant snippet from the list, by clicking on it.

 
  ![Right-click where you want to insert the code snippet and select Insert Snippet](./images/Right-click-where-you-want-to-insert-the-code-snippet-and-select-Insert-Snippet.png?raw=true "Right-click where you want to insert the code snippet and select Insert Snippet")
 
_Right-click where you want to insert the code snippet and select Insert Snippet_

 ![Pick the relevant snippet from the list, by clicking on it](./images/Pick-the-relevant-snippet-from-the-list,-by-clicking-on-it.png?raw=true "Pick the relevant snippet from the list, by clicking on it")
 
_Pick the relevant snippet from the list, by clicking on it_