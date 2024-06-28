/******************************************************************************
     * File:        Class1.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file executes the SeatReserve-ProDBService InitDB-method
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

using SeatReserve_Pro_DBService;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var dbService = new SeatReserve_ProDBService();
dbService.InitDB();