# AxaTest

- In order to to have a better runtime performance the call to the database is async
- To deal with the fact that we have a dataset in the excess of 1,000,000, rather than loading all the data from the db and work on them; we are using a stored procedure that we are calling from the Data Access Layer. This will ensure that we are only retrieving the cities needed. This will prevent an overload and the store procedure will also make the call to the db more performant
- we have added a test project to test the different scenario
- We are also using a console interface to simulate the search string the user will enter.
