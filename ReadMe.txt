Author: 		Jason Amador
Purpose:		Rewrite TCRC web application that processes claims against the Travel Consumer Restitution Fund
Details:		This web application was written on the .NET platform using C# and MVC along with a SQL Server
				database backend with Entity Framework to handle data access.  Object to object mapping is handled
				by AutoMapper.  HTML, CSS, javascript, jQuery, Pure.CSS, and Kendo UI were used for the front end.
				Inversion of control is managed by Unity.
Notes:			This project is for a code sample and will not build for a few reasons:
				1. The database is not included and the entities are not wired up
				2. Some dependencies have not been added in (Unity and AutoMapper)