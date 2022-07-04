# HackerNews
An API with cache for a returning top 20 news from HackerNews.

![GitHub repo size](https://img.shields.io/github/repo-size/grgoncal/HackerNews?style=flat-square)

## Dependencies

- .NET Core 3.1
- DockerHub

## Pre-running

Before trying to run this application, you'll need to run Redis in DockerHub. DockerHub is available to download here: [DockerHub](https://hub.docker.com/editions/community/docker-ce-desktop-windows). 

After installing DockerHub, run the following commands from any command prompt (I used Git Bash):

```
docker run --name=HackerNewsRedis --publish=6379:6379 --hostname=redis --restart=on-failure --detach redis:latest
```

You can see if everything is set up directly on DockerHub app, in "Containers/Apps" tab. 

Lastly, run the app (this project uses swagger).

## Kubernetes container

Create an docker image using the following command in the command prompt:

```
docker build -t hackernews.api:v1 .
```

To check if the image was created you can use:

```
docker images
```

Our image should be on the list with the same name we specified: <i>hackernews.api</i>.

Then, apply our deployment and our service on kubernetes using the commands below:

```
kubectl apply -f kubernetes.dev.yaml
kubectl apply -f service.yaml
```

It is possible to check if our pod and service are online using:

```
kubectl get pods
kubectl get services
```

It's also possible to see our pod online in <i>DockerHub Containers/Apps</i> tab.

## Decisions

### Mediator
Mediator is a design pattern that is capable to loose coupling between ports/controllers, business logic, domain entities and infrastructure. It uses more memory when compared to a service and this can lead to more often GC runs on a pod, causing the application to halt more often.<br><br>
That being said, I do not believe that this characteristic should be a decising factor for a application such as this one (which is not a high frequency/low latency API). <br><br>
On the other hand, its usage will generally lead to cleaner code and facilitates error handling/monitoring features. Un an ever-expanding project, a clean code will be as important as it's responsiveness.

### Redis + Cache
In order to avoid overloading the HackerNews API and as an effort to make the API as fast as possible, I am caching the top 20 stories in Redis and inside a in-memory service. <br><br>

The Redis persistency is useful to maintain low usage on the HackerNews API in scenarios where a pod running this api crashes. It is usefull when the app is deployed any some degree of parallelism (lambda/k8n) and also when using Horizontal Pod Autoscaling (for high volume requisitions). In this scenario, the load on the HackerNews is further reduced since the other pods will load the news from the cache before trying to get the information directly from HackerNews API. <br><br>

### 15 minutes cache
I hard coded a default 15 minutes cache for storing the top 20 news. This value is not the ideal and is only representing an idea. The correct approach is to study the data, checking its sazonality and how it varies overtime. Some other questions would need an answer: <br><br>
1 - How online the user needs the data? <br>
2 - In case 1 is true, is it possible to have a dynamic timing for the cache using the data? 

## To do list / Improvements

1 - Ideally, sorting the news should be a server-side feature. We should be able to query the top <i>n</i> news from HackerNews.<br>
2 - The 15-minute cache is simply a decision made for debugging sake. The data/user requirements should be revisited to further improve this behavior.<br>
3 - Before each request there is a Policy that retries the request before throwing an error. While getting the story details the code will paralellize its execution. It is possible that one or more get requests fail more than 3 times and this will ruin the processing. There should be a threatment to retry these requests at least a couple more times. Alternatively it may be possible to discard the failed requests and continue the processing, but if this happens, dynamic cache timing (the 15 minute fixed interval) is mandatory.<br>
4 - Cacheing the news should be a job that executes each <i>x</i> minutes. This would make the api request extremely fast since it won't need to fetch the news.

## Final considerations

1 - Monitoring should be added to keep track of errors (datadog, dynatrace etc)<br>
2 - <s>I did not add any k8n file, or cloud formation stuff since I don't think it would be useful for anything in this context </s>.<br>
2A - I recently had some time to spare and added it. <br>
3 - I used Sonar to help with code quality. <br>
4 - Making this was really fun! (: Took me <s>3~ hours</s> to make it. <br>
4A - 3 hours originally, now I'm at the 20 hour ballpark between studing stuff and coding. Still fun though.
