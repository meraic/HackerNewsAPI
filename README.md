# This is the HackerNewsApi

The hacker new api, is a RESTful API to retrieve the details of the best n stories from the Hacker News API, as determined by their score.

## How to run the app
    --mkdir \HackerNewsAPI
    - git clone https://github.com/edsondiasalves/hackernewsapi.git]
    - dotnet run --project \HackerNewsAPI/HackerNews.Api.csproj 

## How to run the unit tests
    - dotnet test

## How to get the data
    - curl GET 'https://localhost:7056/v1.0/hackernews?nStories=50'

## Assumptions
  - The Hacker News API currently returns only top 500 best stories. If the API can return more then other scenario such as server side paging can be implemented
    
## Other Enhancements

There is a bunch of enhancements can be done.

 - More unit tests can be added. I have just added one service test due to time constraint
 - Currently I have used in memory LazyCache but Redis can be implemented
 - I can create brand new HackerNewsClient and wrap all the logic to access the HackerNewsAPI in the client and then service can use the newly created client. At the moment, I have wrapped it in service
 - Logging in terms of serilog to file/azure/sql can be done
 
