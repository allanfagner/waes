- How to use the app
	- Just fire up Wase app and the endpoints will be work as requested. Ex:
		- <host>/v1/diff/1/left
		- <host>/v1/diff/1/right
		- <host>/v1/diff/1
	- The actual base 64 string must be sent via json post in a variable named data
	- if the left or right endpoint is called more then once the data will be updated

- Tests
	- The unity tests run in isolation
	- To run the functional tests fire up the app and change the host variable in FunctionalTests type

- It was not very clear what what returning would be errors and what would not. So I decided to return them all as success. That can be easily changed
- I decided to return (when is the case) the offset, lenght e charaters that differ. Expading the assignment requirement
- Entity Framework should create the database. If it does not the scritps to create the tables are in the folder SqlScripts. The database name must be Wase and the password test. Check WaseContext type for reference if needed.
- Giving this is a really small smaple app, and to keep it simple, I decided to create only one project and do not use techniques such as inversion of control and filters for controlling crosscuttings. Although I am familiar with them.
- At this point I did not feel the need of throwing interfaces to the mix
- There are other comments in code itsefl.
- Feel free to be in touch if you need any further info