#Analysis

I tried to predict which movies will win the Oscar for best picture category this year, and whether there is a connection between winning an Oscar and certain features. I tried to analyze the data, and see the most influential features of a movie on winning an Oscar.

thanks to the project  [and-the-award-goes-to](https://github.com/scruwys/and-the-award-goes-to),
from which the data was taken 
The data features included: 

-	Other awards show: BAFTA, Golden Globe, Guild
-	Running Time
-	Box Office
-	Budget
-	IMDb score
-	RT audience score
-	RT critic score
-	Number of actors
-	Number of writers
-	Release date
-	MPAA

##Other Awards Show

It is known that other ceremonies serve as a kind of predictor for Oscar films. 
If a film won the Golden Globes, BAFTA and Guild, chances are he will win an Oscar as well. or not? 
To test this, we used Fisher's exact test on these variables. Here are the results.

<img  src="~/Images/tab1.png" />

###Fisher's Exact Test

BAFTA | p-value = 6.362e-05 | 95 percent confidence interval: 2.284972 to 14.175734 | odds ratio: 5.695619  
Guild | p-value = 0.0002651 | 95 percent confidence interval: 2.194201 to 19.321418 | odds ratio:  6.488601  
Golden Globe | p-value = 3.817e-07 | 95 percent confidence interval:3.084258 to 16.737340 | odds ratio: 7.041181 

As you can see the P-value in all 3, is smaller than 0.05, which mean the alternative hypothesis is true- there is a connection between winning an Oscar and winning in other awards show.                                                  
Which one of these awards ceremonies are the best predictor? Probably the Golden Globe with 7.041181 odds ratio.

##MPAA

The MPAA administers a motion picture rating system used in the United States to rate the suitability of films' themes and content for certain audiences.
“G” – for general audiences, “PG” – parental guidance suggested, “PG-13” – parents strongly cautioned, “R” – restricted.
Does the rate of suitability have an impact on winning an Oscar?
Let's take look at the distribution of data.

<img src="~/Images/tab2.png" />
<img src="~/Images/Rplot.png" />

MPAA | p-value = 0.8042
the p-value is very high, so there seems to be no connection between winning an Oscar and rating of the film.
However, the number of R films that were nominated and won an Oscar is significantly higher than the other ratings. so its most likely that if you want to win an Oscar, you should do an R film.

##Release Date

<img src="~/Images/Rplot01.png" />

I checked the dates of the films' release.
It is noticeable that the films that are being released in December have the most Oscar nominations. Is there a connection between the two? Are the producers deliberately publishing films late, so that the Academy and the audience will remember them? 


