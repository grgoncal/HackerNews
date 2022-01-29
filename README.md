# HackerNews
An API with cache for a returning top 20 news from HackerNews.

![GitHub repo size](https://img.shields.io/github/repo-size/grgoncal/HackerNews?style=flat-square)

## Dependencies

- .NET Core 3.1
- DockerHub

## Pre-running

Before trying to run this application, you'll need to run Redis in DockerHub. DockerHub is available to download here: [DockerHub](https://hub.docker.com/editions/community/docker-ce-desktop-windows).

After installing DockerHub, run the following commands from any command prompt:

```
docker pull redis
```

```
docker run --name HackerNewsRedis -d -p 6379:6379 redis
```

You can see if everything is set up directly on DockerHub app, in "Containers/Apps" tab. Finnaly, run the app (this project uses swagger).

## Decisions

### Mediator
Mediator is a design pattern that is capable to loose coupling between ports/controllers, business logic, domain entities and infrastructure. It uses more memory when compared to a service, and it also can lead to more often GC runs on a pod.<br><br>
That being said, I do not believe that this characteristic should be a decising factor for a application such as this one (which is not a high frequency/low latency API). <br><br>
On the other hand, its usage will generally lead to cleaner code and facilitates error handling/monitoring features. In an ever-expanding project, a clean code will be as important as it's responsiveness.

### Redis + Cache
In order to avoid overloading on the HackerNews API and as an effor to make the API as fast as possible, I am caching the top 20 stories both in Redis and inside a Service (in-memory). <br><br>

The Redis persistency is necessary to maintain low usage on the HackerNews API in case a pod/instance running this app dies. It is also usefull when app is deployed with some degree of parallelism (lambda/k8n). In this scenario, the load on the HackerNews is further reduced since the other pods will load cache (if any) before trying to get the information from HackerNews.  <br><br>

I also opted to add a in memory cache to reduce latency.

### 15 minutes cache
I hard coded a default 15 minutes cache for storing the top 20 news. This value is not the ideal and is only representing an idea. The correct approach is to study the data, checking its sazonality and how it varies overtime. Some other questions would need an answer: <br><br>
1 - How online the user needs the data? <br>
2 - In case 1 is true, is it possible to have a dynamic timing for the cache using the data? 

## To do list / Improvements

1 - Ideally, sorting the news should be a server-side feature. We should be able to query the top n news from HackerNews.<br>
2 - The 15-minute cache is simply a decision made for debugging sake. The data/user requirements should be revisited to further improve this behavior.<br>
3 - Before each request there is a Policy that retries the requisition 3 times before throwing an error. While getting the story details the code will paralellize its execution. It is
possible that one or more get requests fail more than 3 times and this will ruin the processing. There should be a threatment to retry these requests at least a couple more 
times. Alternatively it may be possible to discard the failed requests and continue the processing, but if this happens, dynamic cache timing (the 15 minute fixed interval) 
is mandatory.<br>

## Final considerations

1 - Monitoring should be added to keep track of errors (datadog, dynatrace etc)<br>
2 - I did not add any k8n file, or cloud formation stuff since I don't think it would be useful for anything in this context.<br>
3 - I used Sonar to help with code quality. <br>
4 - Making this was really fun! (: Took me 3~ hours to make it. 
