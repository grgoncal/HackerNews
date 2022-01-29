–
oC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Application\Controllers\HackerNewsController.cs
	namespace 	

HackerNews
 
. 
API 
. 
Application $
.$ %
Controllers% 0
{ 
[ 
Route 

(
 
$str 
) 
] 
public 

class  
HackerNewsController %
:& '

Controller( 2
{		 
private

 
readonly

 
	IMediator

 "
	_mediator

# ,
;

, -
public  
HackerNewsController #
(# $
	IMediator$ -
mediator. 6
)6 7
{ 	
	_mediator 
= 
mediator  
;  !
} 	
[ 	
HttpGet	 
( 
$str 
)  
]  !
public 
IActionResult 
GetTop20News )
() *
)* +
{ 	
var 
getTop20NewsCommand #
=$ %
new& )
GetTop20NewsCommand* =
(= >
)> ?
;? @
var 
response 
= 
	_mediator $
.$ %
Send% )
() *
getTop20NewsCommand* =
)= >
.> ?
Result? E
;E F
if 
( 
! 
string 
. 
IsNullOrEmpty %
(% &
response& .
.. /
Error/ 4
)4 5
)5 6
return 

StatusCode !
(! "
$num" %
,% &
response' /
)/ 0
;0 1
return 
Ok 
( 
response 
) 
;  
} 	
[ 	
HttpGet	 
( 
$str 
) 
]  
public 
IActionResult 
HealthCheck (
(( )
)) *
{ 	
return   
Ok   
(   
)   
;   
}!! 	
}"" 
}## ï
sC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Application\Mediator\Base\AbstractRequestHandler.cs
	namespace

 	

HackerNews


 
.

 
API

 
.

 
Application

 $
.

$ %
Mediator

% -
.

- .
Base

. 2
{ 
public 

abstract 
class "
AbstractRequestHandler 0
<0 1
T1 2
>2 3
:4 5
IRequestHandler6 E
<E F
TF G
,G H
ResponseI Q
>Q R
whereS X
TY Z
:[ \
IRequest] e
<e f
Responsef n
>n o
{ 
	protected 
readonly 
ILogger "
_logger# *
;* +
	protected "
AbstractRequestHandler (
(( )
ILogger) 0
logger1 7
)7 8
{ 	
_logger 
= 
logger 
; 
} 	
internal 
abstract 
Task 
< 
Response '
>' (
HandleRequest) 6
(6 7
T7 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
;f g
public 
async 
Task 
< 
Response "
>" #
Handle$ *
(* +
T+ ,
request- 4
,4 5
CancellationToken6 G
cancellationTokenH Y
)Y Z
{ 	
var 
response 
= 
new 
Response '
(' (
)( )
;) *
try 
{ 
var 
result 
= 
await "
HandleRequest# 0
(0 1
request1 8
,8 9
cancellationToken: K
)K L
;L M
ParseResult 
( 
response $
,$ %
result& ,
), -
;- .
} 
catch   
(   
	Exception   
ex   
)    
{!! 
_logger"" 
."" 
Error"" 
("" 
$"""  
[""  !
{""! "
this""" &
.""& '
GetType""' .
("". /
)""/ 0
.""0 1
Name""1 5
}""5 6-
!] Error while executing command: ""6 W
{""W X
ex""X Z
}""Z [
"""[ \
)""\ ]
;""] ^
}%% 
return'' 
response'' 
;'' 
}(( 	
private** 
void** 
ParseResult**  
(**  !
Response**! )
response*** 2
,**2 3
Response**4 <
result**= C
)**C D
{++ 	
if,, 
(,, 
result,, 
!=,, 
null,, 
&&,, !
result,," (
?,,( )
.,,) *
Error,,* /
==,,0 2
null,,3 7
),,7 8
response-- 
.-- 
Content--  
=--! "
result--# )
.--) *
Content--* 1
;--1 2
else.. 
if.. 
(.. 
result.. 
!=.. 
null.. #
&&..$ &
result..' -
?..- .
.... /
Error../ 4
!=..5 7
null..8 <
)..< =
response// 
.// 
Error// 
=//  
result//! '
.//' (
Error//( -
;//- .
}00 	
}11 
}22 ≈
ÅC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Application\Mediator\Commands\HackerNews\CacheTop20NewsCommand.cs
	namespace 	

HackerNews
 
. 
API 
. 
Application $
.$ %
Mediator% -
.- .
Commands. 6
.6 7

HackerNews7 A
{		 
public

 

class

 !
CacheTop20NewsCommand

 &
:

' (
IRequest

) 1
<

1 2
Response

2 :
>

: ;
{ 
} 
} Ç9
àC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Application\Mediator\Commands\HackerNews\CacheTop20NewsCommandHandler.cs
	namespace 	

HackerNews
 
. 
API 
. 
Application $
.$ %
Mediator% -
.- .
Commands. 6
.6 7

HackerNews7 A
{ 
public 

class (
CacheTop20NewsCommandHandler -
:. /"
AbstractRequestHandler0 F
<F G!
CacheTop20NewsCommandG \
>\ ]
{ 
private 
readonly 
IHackerNewsRedis )
_hackerNewsRedis* :
;: ;
private 
readonly 
IHackerNewsService +
_hackerNewsService, >
;> ?
private 
readonly 
object 
_locker  '
=( )
new* -
object. 4
(4 5
)5 6
;6 7
public (
CacheTop20NewsCommandHandler +
(+ ,
IHackerNewsRedis, <
hackerNewsRedis= L
,L M
IHackerNewsService 
hackerNewsService 0
,0 1
ILogger 
logger 
) 
: 
base "
(" #
logger# )
)) *
{ 	
_hackerNewsRedis 
= 
hackerNewsRedis .
;. /
_hackerNewsService 
=  
hackerNewsService! 2
;2 3
} 	
internal 
override 
Task 
< 
Response '
>' (
HandleRequest) 6
(6 7!
CacheTop20NewsCommand7 L
requestM T
,T U
CancellationTokenV g
cancellationTokenh y
)y z
{   	
var!! 
idList!! 
=!!  
GetBestHistoryIdList!! -
(!!- .
)!!. /
;!!/ 0
var"" 
newsList"" 
="" 
GetHistoriesDetails"" .
("". /
idList""/ 5
)""5 6
;""6 7
var$$ 
	top20News$$ 
=$$ 
newsList$$ $
.$$$ %
OrderByDescending$$% 6
($$6 7
n$$7 8
=>$$9 ;
n$$< =
.$$= >
Score$$> C
)$$C D
.$$D E
Take$$E I
($$I J
$num$$J L
)$$L M
.$$M N
ToList$$N T
($$T U
)$$U V
;$$V W
_hackerNewsRedis&& 
.&& 
Add&&  
(&&  !
$str&&! 4
,&&4 5
	top20News&&6 ?
,&&? @
TimeSpan&&A I
.&&I J
FromMinutes&&J U
(&&U V
$num&&V X
)&&X Y
)&&Y Z
;&&Z [
return(( 
new(( 
Response(( 
(((  
	top20News((  )
)(() *
.((* +
GetResponseAsTask((+ <
(((< =
)((= >
;((> ?
})) 	
private++ 
List++ 
<++ 
New++ 
>++ 
GetHistoriesDetails++ -
(++- .
List++. 2
<++2 3
long++3 7
>++7 8
idList++9 ?
)++? @
{,, 	
var-- 
newsDetailsList-- 
=--  !
new--" %
List--& *
<--* +
New--+ .
>--. /
(--/ 0
)--0 1
;--1 2
var// 
taskList// 
=// 
new// 
List// #
<//# $
Task//$ (
>//( )
(//) *
)//* +
;//+ ,
foreach00 
(00 
var00 
id00 
in00 
idList00 %
)00% &
taskList11 
.11 
Add11 
(11 
GetHistoryDetail11 -
(11- .
id11. 0
,110 1
newsDetailsList112 A
)11A B
)11B C
;11C D
var33 
taskAwaiter33 
=33 
Task33 "
.33" #
WhenAll33# *
(33* +
taskList33+ 3
)333 4
;334 5
taskAwaiter44 
.44 
Wait44 
(44 
)44 
;44 
if66 
(66 
taskAwaiter66 
.66 
Status66 "
==66# %

TaskStatus66& 0
.660 1
Faulted661 8
)668 9
_logger77 
.77 
Error77 
(77 
$str77  
)77  !
;77! "
return99 
newsDetailsList99 "
;99" #
}:: 	
private<< 
Task<< 
GetHistoryDetail<< %
(<<% &
long<<& *
id<<+ -
,<<- .
List<</ 3
<<<3 4
New<<4 7
><<7 8
newsDetailsList<<9 H
)<<H I
{== 	
return>> 
Task>> 
.>> 
Run>> 
(>> 
(>> 
)>> 
=>>> !
{?? 
var@@ 
	newDetail@@ 
=@@ 
GetNewDetail@@  ,
(@@, -
id@@- /
)@@/ 0
;@@0 1
lockBB 
(BB 
_lockerBB 
)BB 
{CC 
ifDD 
(DD 
!DD 
newsDetailsListDD (
.DD( )
ContainsDD) 1
(DD1 2
	newDetailDD2 ;
)DD; <
)DD< =
newsDetailsListEE '
.EE' (
AddEE( +
(EE+ ,
	newDetailEE, 5
)EE5 6
;EE6 7
}FF 
}GG 
)GG 
;GG 
}HH 	
privateJJ 
NewJJ 
GetNewDetailJJ  
(JJ  !
longJJ! %
idJJ& (
)JJ( )
{KK 	
varLL 
	newDetailLL 
=LL 
newLL 
NewLL  #
(LL# $
)LL$ %
;LL% &

SafeCallerNN 
.NN 
SafeCallNN 
(NN  
(NN  !
)NN! "
=>NN# %
{OO 
	newDetailPP 
=PP 
_hackerNewsServicePP .
.PP. /
GetNewDetailsPP/ <
(PP< =
idPP= ?
)PP? @
;PP@ A
}QQ 
,QQ 
_loggerQQ 
)QQ 
;QQ 
returnSS 
	newDetailSS 
;SS 
}TT 	
privateVV 
ListVV 
<VV 
longVV 
>VV  
GetBestHistoryIdListVV /
(VV/ 0
)VV0 1
{WW 	
varXX 
idListXX 
=XX 
newXX 
ListXX !
<XX! "
longXX" &
>XX& '
(XX' (
)XX( )
;XX) *

SafeCallerZZ 
.ZZ 
SafeCallZZ 
(ZZ  
(ZZ  !
)ZZ! "
=>ZZ" $
{[[ 
idList\\ 
=\\ 
_hackerNewsService\\ +
.\\+ ,%
GetListOfBestHistoriesIds\\, E
(\\E F
)\\F G
;\\G H
}]] 
,]] 
_logger]] 
)]] 
;]] 
return__ 
idList__ 
;__ 
}`` 	
}aa 
}bb ¿
C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Application\Mediator\Commands\HackerNews\GetTop20NewsCommand.cs
	namespace 	

HackerNews
 
. 
API 
. 
Application $
.$ %
Mediator% -
.- .
Commands. 6
.6 7

HackerNews7 A
{ 
public 

class 
GetTop20NewsCommand $
:% &
IRequest' /
</ 0
Response0 8
>8 9
{ 
} 
}		 ˇ
ÜC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Application\Mediator\Commands\HackerNews\GetTop20NewsCommandHandler.cs
	namespace 	

HackerNews
 
. 
API 
. 
Application $
.$ %
Mediator% -
.- .
Commands. 6
.6 7

HackerNews7 A
{ 
public 

class &
GetTop20NewsCommandHandler +
:, -"
AbstractRequestHandler. D
<D E
GetTop20NewsCommandE X
>X Y
{ 
private 
readonly 
	IMediator "
	_mediator# ,
;, -
private 
readonly 
IHackerNewsRedis )
_hackerNewsRedis* :
;: ;
public &
GetTop20NewsCommandHandler )
() *
	IMediator* 3
mediator4 <
,< =
IHackerNewsRedis 
hackerNewsRedis ,
,, -
ILogger 
logger 
) 
: 
base "
(" #
logger# )
)) *
{ 	
	_mediator 
= 
mediator  
;  !
_hackerNewsRedis 
= 
hackerNewsRedis .
;. /
} 	
internal 
override 
Task 
< 
Response '
>' (
HandleRequest) 6
(6 7
GetTop20NewsCommand7 J
requestK R
,R S
CancellationTokenT e
cancellationTokenf w
)w x
{ 	
var 
	top20News 
= 
_hackerNewsRedis ,
., -
Get- 0
(0 1
$str1 D
)D E
;E F
if!! 
(!! 
	top20News!! 
==!! 
null!! !
)!!! "
{"" 
var## !
cacheTop20NewsCommand## )
=##* +
new##, /!
CacheTop20NewsCommand##0 E
(##E F
)##F G
;##G H
var$$ 
result$$ 
=$$ 
	_mediator$$ &
.$$& '
Send$$' +
($$+ ,!
cacheTop20NewsCommand$$, A
)$$A B
.$$B C
Result$$C I
;$$I J
	top20News%% 
=%% 
ParseResult%% '
(%%' (
result%%( .
)%%. /
;%%/ 0
}&& 
return(( 
new(( 
Response(( 
(((  
	top20News((  )
)(() *
.((* +
GetResponseAsTask((+ <
(((< =
)((= >
;((> ?
})) 	
private++ 
List++ 
<++ 
New++ 
>++ 
ParseResult++ %
(++% &
Response++& .
result++/ 5
)++5 6
{,, 	
if-- 
(-- 
result-- 
==-- 
null-- 
)-- 
throw.. 
new.. 
	Exception.. #
(..# $
$"..$ &3
'Error ocurred while getting top 20 news..& M
"..M N
)..N O
;..O P
if00 
(00 
result00 
!=00 
null00 
&&00 !
!00" #
string00# )
.00) *
IsNullOrEmpty00* 7
(007 8
result008 >
.00> ?
Error00? D
)00D E
)00E F
throw11 
new11 
	Exception11 #
(11# $
$"11$ &5
)Error ocurred while getting top 20 news: 11& O
{11O P
result11P V
.11V W
Error11W \
}11\ ]
"11] ^
)11^ _
;11_ `
return33 
result33 
.33 
Content33 !
as33" $
List33% )
<33) *
New33* -
>33- .
;33. /
}44 	
}55 
}66 ©!
iC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Extensions\ServiceCollectionExtensions.cs
	namespace 	

HackerNews
 
. 
API 
. 

Extensions #
{ 
public 

static 
class '
ServiceCollectionExtensions 3
{ 
public 
static 
IServiceCollection (
AddDependencies) 8
(8 9
this9 =
IServiceCollection> P
serviceCollectionQ b
,b c
IConfigurationd r
configuration	s Ä
)
Ä Å
{ 	
serviceCollection 
. 
	Configure '
<' (
AppSettings( 3
>3 4
(4 5
configuration5 B
)B C
;C D
serviceCollection 
. 
AddMvc $
($ %
)% &
;& '
serviceCollection 
. 
AddControllers ,
(, -
)- .
;. /
serviceCollection 
. 
AddSwaggerGen +
(+ ,
swagger, 3
=>4 6
{ 
swagger 
. 

SwaggerDoc "
(" #
$str# '
,' (
new) ,
	Microsoft- 6
.6 7
OpenApi7 >
.> ?
Models? E
.E F
OpenApiInfoF Q
{R S
TitleT Y
=Z [
$str\ l
,l m
Versionn u
=v w
$strx |
}} ~
)~ 
;	 Ä
}   
)   
;   
serviceCollection"" 
."" 

AddMediatR"" (
(""( )
typeof"") /
(""/ 0
Startup""0 7
)""7 8
)""8 9
;""9 :
serviceCollection## 
.## 

AddMediatR## (
(##( )
typeof##) /
(##/ 0
IRequestHandler##0 ?
<##? @
>##@ A
)##A B
)##B C
;##C D
serviceCollection$$ 
.$$ 

AddMediatR$$ (
($$( )
typeof$$) /
($$/ 0
IRequestHandler$$0 ?
<$$? @
,$$@ A
>$$A B
)$$B C
)$$C D
;$$D E
serviceCollection%% 
.%% 

AddMediatR%% (
(%%( )
typeof%%) /
(%%/ 0 
INotificationHandler%%0 D
<%%D E
>%%E F
)%%F G
)%%G H
;%%H I
serviceCollection'' 
.'' 
TryAddSingleton'' -
<''- . 
IHttpContextAccessor''. B
,''B C
HttpContextAccessor''D W
>''W X
(''X Y
)''Y Z
;''Z [
serviceCollection(( 
.(( 
TryAddSingleton(( -
<((- ."
IActionContextAccessor((. D
,((D E!
ActionContextAccessor((F [
>(([ \
(((\ ]
)((] ^
;((^ _
serviceCollection** 
.** 
AddSingleton** *
<*** +
ILogger**+ 2
,**2 3
Logger**4 :
>**: ;
(**; <
)**< =
;**= >
serviceCollection,, 
.,, 
AddTransient,, *
<,,* +
IHackerNewsClient,,+ <
,,,< =
HackerNewsClient,,> N
>,,N O
(,,O P
),,P Q
;,,Q R
serviceCollection-- 
.-- 
AddTransient-- *
<--* +
IHackerNewsService--+ =
,--= >
HackerNewsService--? P
>--P Q
(--Q R
)--R S
;--S T
serviceCollection// 
.// 
AddSingleton// *
<//* +#
IRedisConnectionFactory//+ B
,//B C"
RedisConnectionFactory//D Z
>//Z [
(//[ \
)//\ ]
;//] ^
serviceCollection00 
.00 
AddSingleton00 *
<00* +
IHackerNewsRedis00+ ;
,00; <
HackerNewsRedis00= L
>00L M
(00M N
)00N O
;00O P
return22 
serviceCollection22 $
;22$ %
}33 	
}44 
}55 å	
JC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Program.cs
	namespace 	

HackerNews
 
. 
API 
{ 
public 

static 
class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{		 	
BuildWebHost

 
(

 
args

 
)

 
.

 
Run

 "
(

" #
)

# $
;

$ %
} 	
private 
static 
IWebHost 
BuildWebHost  ,
(, -
string- 3
[3 4
]4 5
args6 :
): ;
=>< >
WebHost 
.  
CreateDefaultBuilder (
(( )
args) -
)- .
. 

UseStartup 
< 
Startup &
>& '
(' (
)( )
. 
Build 
( 
) 
; 
} 
} ﬁ
JC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.API\Startup.cs
	namespace 	

HackerNews
 
. 
API 
{		 
public

 

class

 
Startup

 
{ 
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
Startup 
( 
IConfiguration %
configuration& 3
,3 4
IWebHostEnvironment5 H
envI L
)L M
{ 	
var 
buider 
= 
new  
ConfigurationBuilder 1
(1 2
)2 3
.3 4
SetBasePath4 ?
(? @
env@ C
.C D
ContentRootPathD S
)S T
.2 3
AddJsonFile3 >
(> ?
$str? Q
,Q R
optionalS [
:[ \
false] b
,b c
reloadOnChanged r
:r s
truet x
)x y
.2 3
AddJsonFile3 >
(> ?
$"? A
appsettings.A M
{M N
envN Q
.Q R
EnvironmentNameR a
}a b
.jsonb g
"g h
,h i
optionalj r
:r s
truet x
)x y
.2 3#
AddEnvironmentVariables3 J
(J K
)K L
;L M
Configuration 
= 
buider "
." #
Build# (
(( )
)) *
;* +
} 	
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
serviceCollection9 J
)J K
{ 	
serviceCollection 
. 
AddDependencies -
(- .
Configuration. ;
); <
;< =
} 	
public 
void 
	Configure 
( 
IApplicationBuilder 1
app2 5
,5 6
IWebHostEnvironment7 J
envK N
)N O
{ 	
app 
. 

UseSwagger 
( 
) 
; 
app   
.   
UseSwaggerUI   
(   
options   $
=>  % '
{!! 
options"" 
."" 
SwaggerEndpoint"" '
(""' (
$str""( B
,""B C
$str""D S
)""S T
;""T U
}## 
)## 
;## 
if%% 
(%% 
env%% 
.%% 
IsDevelopment%% !
(%%! "
)%%" #
)%%# $
app&& 
.&& %
UseDeveloperExceptionPage&& -
(&&- .
)&&. /
;&&/ 0
app(( 
.(( 

UseRouting(( 
((( 
)(( 
;(( 
app)) 
.)) 
UseEndpoints)) 
()) 
	endpoints)) &
=>))' )
{** 
	endpoints++ 
.++ 
MapControllers++ (
(++( )
)++) *
;++* +
},, 
),, 
;,, 
app.. 
... 
UseStatusCodePages.. "
(.." #
)..# $
;..$ %
}// 	
}00 
}11 