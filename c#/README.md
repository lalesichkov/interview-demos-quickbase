# Materials for developer interviews for Quickbase

## Purpose
The purpose of this exercise is not to give a "gotcha" question or puzzle, but a straight-forward (albeit contrived)
example of the kind of requirement that might arise in a real project so that we have shared context for a technical 
conversation during the interview. We are interested in how you approach a project, so you should feel free to add new 
class files as well modify the files that are provided as you see fit. Use of your favorite libraries or frameworks is
encouraged, but not required. How you demonstrate the correctness of your implementation is up to you.

## Requirements
The project requirement is to aggregate data (in this case population statistics) from two disparate sources.
We've provided two classes to represent those sources. `SqliteDbManager.cs`, provides access to a SQL database containing population
data for cities.  Each city is in a state within a country.  You need to write a method to retrieve the total
population for each country.  The other class, `IStatService.cs`, returns a `List<Tuple<String, Integer>>` containing 
country population data. For the purposes of this exercise, we've provided a concrete class that just returns a 
hard-coded list, but in a real project, assume it would be calling an API.

The assignment is to implement a solution that consumes these two data sources and returns the combined list of
countries and their populations. In the event of duplicate population data for a given country, the data from
the sql database should be used. 

## Building and Running the code

This project assumes you're using Visual Studio 2022 or newer and depends on nuget.

That said, feel free to challenge any of the current limitations with your demo. Just keep the time limit in mind.

## Explanation of proposed solution
The proposed solution combines the data from the two sources by first retrieving the population data from the SQL database
and storing it in a dictionary. Then, it retrieves the population data from the `IStatService` 
and updates the dictionary with any missing countries or population data. Finally, it returns (writes to the console) 
the final list of dictionary entries containing the country names and their corresponding populations. 

## Edge cases and assumptions
- The SQL database may contain duplicate entries for a country, in which case the population data from the first entry will be used.
- There is no unified format for country names between the two data sources, so the solution proposes a simple normalization step to ensure that country names are compared in a case-insensitive manner and with leading/trailing whitespace removed (e.g. U.S.A and United States of America should be treated as the same entry).
- The solution assumes that countries which are present in the `IStatService` with parenthesis in their names which supposes a dominion or territory (e.g. Aruba (Netherlands)) should be treated as separate entities from their parent countries (e.g. Netherlands).