# The Report

To maximize the highest level of accuracy, I used several decision tree algorithms: random forests, random decision forests and C45 decision tree. Then I compared the algorithms with each other to choose the best one.

I used a logistic regression model that returned the movie with the highest probability of winning among those the algorithms predicted as winners. Using this model, the system declared the winner.

To calculate the accuracy of each one of these algorithms, I created a function that used a confusion matrix (by Accord.Net Framework). 
In each iteration, I calculated the accuracy and weighed the results. 

![alt text](WebApplication1/Images/g1.png)

![alt text](WebApplication1/Images/g3.png)


As per the confusion matrix, the results of the algorithms are almost identical and there is no significant difference.

### Does the time affect the outcome? 
I reduced the size of the data by choosing the years between 2006-2016.

![alt text](WebApplication1/Images/g4.png)

After narrowing the range of years the accuracy is better. Time seems to have a significant impact. Movies that have won in the past, probably would not win today.

The accuracy now is 84% for C45, and 85% for Random Forest.

### Cross-Validation

To double check the calculation of the algorithm's accuracy, i used  cross-validation to estimate the performance of the decision tree model. Here are the results:

![alt text](WebApplication1/Images/g5.JPG)

See that the results are no different.
