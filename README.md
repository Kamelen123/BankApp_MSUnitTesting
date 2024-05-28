In this project, I tested three methods in my group's Banking Application: CurrencySelectionMenu(), IsValidBorrowAmount(), and ConvertCurrency().

CurrencySelectionMenu is a method designed to ensure that the user selects a valid currency. There are three valid outcomes: USD, EURO, and KRONOR. If none of these are selected, the method will loop until one of these currencies is selected.
To test this method, I constructed four test methods: one for each currency and one method to test the intended function, which is to guarantee the selection of a valid currency. I had to change a few lines of code in the original method in order to handle inputs from the test method. All the tests passed, and the method works as intended. However, it became apparent that there should be a way to back out of the menu.

IsValidBorrowAmount is a method that ensures that the user is not allowed to borrow more money than intended. It works by checking if the amount that the user is trying to borrow is greater than zero and then if it's less than the borrowing limit.
I designed three test methods: one where I tested the function's ability to handle amounts equaling or less than zero, one test to see if it handles an acceptable amount, and one test to see how it handles an unacceptable amount, which would be more than the borrowing limit. All tests passed, and the method seems to handle all possible scenarios correctly.

Lastly, I tested the ConvertCurrency method. This method handles conversions between the three currencies when transferring funds from one account to another. I designed six test methods, one for each currency conversion.
This method does not rely on any user input, so the tests only check if the method handles the base conversion correctly. All tests passed except for one where I changed the base value of the conversion rate.
