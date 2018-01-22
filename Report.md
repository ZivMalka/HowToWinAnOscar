# The Report

To predict whether a film would win an Oscar or not, I used Random forests or random decision forests. I wanted to reach the highest level of accuracy, so I examined additional options, such as a decision tree C45 algorithm.

Since these algorithms can predict more than one winner a year, or none winner a year, I  used the Logistic Regression, which chose the film with the highest probability of winning among those that the algorithm predicted as winners.

To calculate the accuracy of the algorithm, I created a function that used a confusion matrix (by Accord.Net Framework). 
In each iteration, I calculated the accuracy and weighed the results. 

Here are the results:

<img src="/Images/g1.png" />

<br /><br /><br />

<img class="img3" src="/Images/g3.png" />


You can see that the results of the algorithms are almost identical and there is no significant difference.

### Does the time affect the outcome? 
I reduced the size of the data and chose the years between 2006 and 2016.

<img class="img2" src="/Images/g4.png" />

As you can see, the results are better, after I have reduced the range of the years. This can be related to the fact that times have changed. Films that have won in the past were not sure to win today. The accuracy now is 84% for C45, and 85% for Random Forest.

### Cross-Validation

I was still not sure whether our calculation of the accuracy was correct, and that is precisely the accuracy of the algorithm. Therefore, we estimate the performance of the decision tree model using cross-validation. Here are the results:

<img  class="img2" src="/Images/g5.jpg" />

You can see the results are no different from previous results.
