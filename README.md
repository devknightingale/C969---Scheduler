# C969 - Scheduler

Requirements
Your submission must be your original work. No more than a combined total of 30% of the submission and no more than a 10% match to any one individual source can be directly quoted or closely paraphrased from sources, even if cited correctly. The similarity report that is provided when you submit your task can be used as a guide.



You must use the rubric to direct the creation of your submission because it provides detailed criteria that will be used to evaluate your work. Each requirement below may be evaluated by more than one rubric aspect. The rubric aspect titles may contain hyperlinks to relevant portions of the course.



Tasks may not be submitted as cloud links, such as links to Google Docs, Google Slides, OneDrive, etc., unless specified in the task requirements. All other submissions must be file types that are uploaded and submitted as attachments (e.g., .docx, .pdf, .ppt).



Note: You are not allowed to use frameworks or external libraries, except for the .NET Framework. The database does not contain data, so it needs to be populated. The word “test” must be used as the username and password to login to the C# application.

1.   Create an application by completing the following tasks in C#:
     -   Create a login form that has the ability to do the following:
          - Determine a user’s location.
          - Translate login and error control messages (e.g., “The username and password do not match.”) into English and one additional language.
          - Verify the correct username and password.
2.   Provide the ability to add, update, and delete customer records.
      -   Validate each of the following requirements for customer records:
           - that a customer record includes name, address, and phone number fields
           - that fields are trimmed and non-empty
           - that the phone number field allows only digits and dashes
      -   Add exception handling that can be used when performing each of the following operations for customer records:
           - “add” operations
           - “update” operations
           - “delete database” operations
3.   Provide the ability to add, update, and delete appointments, capture the type of appointment, and link to a specific customer record in the database.
     -  Validate each of the following requirements for appointments:
          -  Require appointments to be scheduled during the business hours of 9:00 a.m. to 5:00 p.m., Monday–Friday, eastern standard time.
          -  Prevent the scheduling of overlapping appointments.
     -  Add exception handling that can be used when performing each of the following operations for appointments:
          - “add” operations
          - “update” operations
          - “delete database” operations
4.   Create a calendar view feature, including the ability to view appointments on a specific day by selecting a day of the month from a calendar of the months of the year.
5.   Provide the ability to automatically adjust appointment times based on user time zones and daylight saving time.
6.   Create a function that generates an alert whenever a user who has an appointment within 15 minutes logs in to their account.
7.   Create a function that allows users to generate the three reports listed using collection classes, incorporating a lambda expression into the code for each of the following reports:
     -   the number of appointment types by month
     -   the schedule for each user
     -   one additional report of your choice
8.   Record the timestamp and the username of each login in a text file named “Login_History.txt,” ensuring that each new record is appended to the log file.
9.   Submit the project by doing the following:
     -   Export the project in Visual Studio format.
     -   Export your project from the IDE as a ZIP file.
10.   Demonstrate professional communication in the content and presentation of your submission.
