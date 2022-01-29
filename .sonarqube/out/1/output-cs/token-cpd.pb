§
nC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\DataAccess\Redis\Base\RedisBase.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %

DataAccess% /
./ 0
Redis0 5
.5 6
Base6 :
{ 
public 

abstract 
class 
	RedisBase #
<# $
T$ %
>% &
:' (

IRedisBase) 3
<3 4
T4 5
>5 6
{ 
	protected 
	IDatabase 
	_database %
;% &
	protected 
IServer 
_server !
;! "
	protected 
ILogger 
_logger !
;! "
	protected 
	RedisBase 
( 
) 
{ 	
} 	
public 
void 
Add 
( 
string 
key "
," #
object$ *
value+ 0
,0 1
TimeSpan2 :
?: ;
	expiresIn< E
=F G
nullH L
)L M
{ 	
	expiresIn 
= 
	expiresIn !
??" $
TimeSpan% -
.- .
FromMinutes. 9
(9 :
$num: <
)< =
;= >

SafeCaller 
. 
SafeCall 
(  
(  !
)! "
=># %
{ 
var 
content 
= 
JsonConvert )
.) *
SerializeObject* 9
(9 :
value: ?
)? @
;@ A
var 

redisValue 
=  
new! $

RedisValue% /
(/ 0
content0 7
)7 8
;8 9
	_database   
.   
	StringSet   #
(  # $
key  $ '
,  ' (

redisValue  ) 3
,  3 4
	expiresIn  5 >
)  > ?
;  ? @
}!! 
,!! 
_logger!! 
)!! 
;!! 
}"" 	
public$$ 
T$$ 
Get$$ 
($$ 
string$$ 
key$$ 
)$$  
{%% 	
var&& 
result&& 
=&& 
string&& 
.&&  
Empty&&  %
;&&% &

SafeCaller(( 
.(( 
SafeCall(( 
(((  
(((  !
)((! "
=>((# %
{)) 
result** 
=** 
	_database** "
.**" #
StringGetAsync**# 1
(**1 2
key**2 5
)**5 6
.**6 7
Result**7 =
;**= >
}++ 
,++ 
_logger++ 
)++ 
;++ 
if-- 
(-- 
result-- 
==-- 
null-- 
)-- 
return.. 
default.. 
(.. 
T..  
)..  !
;..! "
return00 
JsonConvert00 
.00 
DeserializeObject00 0
<000 1
T001 2
>002 3
(003 4
result004 :
.00: ;
ToString00; C
(00C D
)00D E
)00E F
;00F G
}11 	
}22 
}33 Î
~C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\DataAccess\Redis\Factory\RedisConnectionFactory.cs
	namespace		 	

HackerNews		
 
.		 
Infraestructure		 $
.		$ %

DataAccess		% /
.		/ 0
Redis		0 5
.		5 6
Factory		6 =
{

 
public 

class "
RedisConnectionFactory '
:( )#
IRedisConnectionFactory* A
{ 
[ 	
Redis	 
( 
	Reference 
= 
$str '
)' (
]( )
public 
Lazy 
< !
ConnectionMultiplexer )
>) *!
_hackerNewsConnection+ @
{A B
getC F
;F G
setH K
;K L
}M N
public "
RedisConnectionFactory %
(% &
IOptions& .
<. /
AppSettings/ :
>: ;
settings< D
)D E
{ 	
var 
redisConfigs 
= 
settings '
.' (
Value( -
.- .
RedisConfigs. :
;: ;!
_hackerNewsConnection !
=" #
new$ '
Lazy( ,
<, -!
ConnectionMultiplexer- B
>B C
(C D
(D E
)E F
=>G I
{ 
var 
connectionString $
=% &
redisConfigs' 3
.3 4
FirstOrDefault4 B
(B C
cC D
=>E G
cH I
.I J
	ReferenceJ S
==T V
GetRedisConfigW e
(e f
$strf }
)} ~
)~ 
.	 Ä
ConnectionString
Ä ê
;
ê ë
return !
ConnectionMultiplexer ,
., -
Connect- 4
(4 5
connectionString5 E
)E F
;F G
} 
) 
; 
} 	
public !
ConnectionMultiplexer $#
GetHackerNewsConnection% <
(< =
)= >
{ 	
return !
_hackerNewsConnection (
.( )
Value) .
;. /
} 	
private   
string   
GetRedisConfig   %
(  % &
string  & ,
propertyName  - 9
)  9 :
{!! 	
var"" 
prop"" 
="" 
this"" 
."" 
GetType"" #
(""# $
)""$ %
.""% &
GetProperty""& 1
(""1 2
propertyName""2 >
)""> ?
.""? @
GetCustomAttributes""@ S
(""S T
true""T X
)""X Y
.""Y Z
FirstOrDefault""Z h
(""h i
)""i j
;""j k
if$$ 
($$ 
prop$$ 
is$$ 
RedisAttribute$$ &
)$$& '
return%% 
(%% 
prop%% 
as%% 
RedisAttribute%%  .
)%%. /
.%%/ 0
	Reference%%0 9
;%%9 :
return'' 
string'' 
.'' 
Empty'' 
;''  
}(( 	
})) 
}** ò
oC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\DataAccess\Redis\HackerNewsRedis.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %

DataAccess% /
./ 0
Redis0 5
{		 
public

 

class

 
HackerNewsRedis

  
:

! "
	RedisBase

# ,
<

, -
List

- 1
<

1 2
New

2 5
>

5 6
>

6 7
,

7 8
IHackerNewsRedis

9 I
{ 
public 
HackerNewsRedis 
( #
IRedisConnectionFactory 6"
redisConnectionFactory7 M
)M N
:O P
baseQ U
(U V
)V W
{ 	
var 

connection 
= "
redisConnectionFactory 3
.3 4#
GetHackerNewsConnection4 K
(K L
)L M
;M N
var 
endpoint 
= 

connection %
.% &
GetEndPoints& 2
(2 3
)3 4
.4 5
FirstOrDefault5 C
(C D
)D E
;E F
	_database 
= 

connection "
." #
GetDatabase# .
(. /
)/ 0
;0 1
_server 
= 

connection  
.  !
	GetServer! *
(* +
endpoint+ 3
)3 4
;4 5
} 	
} 
} ´
\C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\Logger\Logger.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %
Logger% +
{ 
public 

class 
Logger 
: 
ILogger !
{		 
public

 
void

 
Debug

 
(

 
string

  
message

! (
)

( )
{ 	
Console 
. 
	WriteLine 
( 
$"  
[DEBUG]   (
{( )
message) 0
}0 1
"1 2
)2 3
;3 4
} 	
public 
void 
Error 
( 
string  
message! (
)( )
{ 	
Console 
. 
	WriteLine 
( 
$"  
[ERROR]   (
{( )
message) 0
}0 1
"1 2
)2 3
;3 4
} 	
public 
void 
Info 
( 
string 
message  '
)' (
{ 	
Console 
. 
	WriteLine 
( 
$"  
[INFO]   '
{' (
message( /
}/ 0
"0 1
)1 2
;2 3
} 	
public 
void 
Warning 
( 
string "
message# *
)* +
{ 	
Console 
. 
	WriteLine 
( 
$"  

[WARNING]   *
{* +
message+ 2
}2 3
"3 4
)4 5
;5 6
} 	
} 
}   ƒ
gC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\Services\Base\RESTClient.cs
	namespace

 	

HackerNews


 
.

 
Infraestructure

 $
.

$ %
Services
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
class 

RESTClient $
:% &
IRESTClient' 2
{ 
private 
readonly 
ILogger  
_logger! (
;( )
	protected 
abstract 
string !

ServiceUrl" ,
{- .
get/ 2
;2 3
}4 5
	protected 

RESTClient 
( 
ILogger $
logger% +
)+ ,
{ 	
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
TResult !
>! "
Get# &
<& '
TResult' .
>. /
(/ 0
string0 6
method7 =
)= >
where 
TResult 
: 
class !
{ 	
var 
result 
= 
default  
(  !
TResult! (
)( )
;) *
using 
( 
var 
client 
= 
new  #

HttpClient$ .
(. /
)/ 0
)0 1
{ 
client 
. 
BaseAddress "
=# $
new% (
Uri) ,
(, -
$"- /
{/ 0

ServiceUrl0 :
}: ;
"; <
)< =
;= >
var 
response 
= 
await $
client% +
.+ ,
GetAsync, 4
(4 5
method5 ;
,; < 
HttpCompletionOption= Q
.Q R
ResponseContentReadR e
)e f
.f g
ConfigureAwaitg u
(u v
falsev {
){ |
;| }
if!! 
(!! 
response!! 
.!! 
IsSuccessStatusCode!! 0
)!!0 1
{"" 
result## 
=## 
	GetResult## &
<##& '
TResult##' .
>##. /
(##/ 0
response##0 8
)##8 9
;##9 :
}$$ 
}%% 
return'' 
result'' 
;'' 
}(( 	
private** 
TResult** 
	GetResult** !
<**! "
TResult**" )
>**) *
(*** +
HttpResponseMessage**+ >
response**? G
)**G H
where**I N
TResult**O V
:**W X
class**Y ^
{++ 	
try,, 
{-- 
var.. 
serializedContent.. %
=..& '
response..( 0
...0 1
Content..1 8
...8 9
ReadAsStringAsync..9 J
(..J K
)..K L
...L M

GetAwaiter..M W
(..W X
)..X Y
...Y Z
	GetResult..Z c
(..c d
)..d e
;..e f
return// 
JsonConvert// "
.//" #
DeserializeObject//# 4
<//4 5
TResult//5 <
>//< =
(//= >
serializedContent//> O
)//O P
;//P Q
}00 
catch11 
(11 
	Exception11 
ex11 
)11  
{22 
_logger33 
.33 
Error33 
(33 
$"33  4
(Error while deserializing get response: 33  H
{33H I
ex33I K
}33K L
"33L M
)33M N
;33N O
throw44 
;44 
}55 
}66 	
}77 
}88 ≤
dC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\Services\Base\Service.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %
Services% -
.- .
Base. 2
{ 
public 

abstract 
class 
Service !
<! "
TClient" )
>) *
where+ 0
TClient1 8
:9 :
class; @
{ 
	protected		 
TClient		 
Client		  
;		  !
	protected 
Service 
( 
TClient !
client" (
)( )
{ 	
Client 
= 
client 
; 
} 	
} 
} ô
sC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\Services\HackerNews\HackerNewsClient.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %
Services% -
.- .

HackerNews. 8
{ 
public 

class 
HackerNewsClient !
:" #

RESTClient$ .
,. /
IHackerNewsClient0 A
{ 
	protected 
readonly 
IOptions #
<# $
AppSettings$ /
>/ 0
	_settings1 :
;: ;
public 
HackerNewsClient 
(  
IOptions  (
<( )
AppSettings) 4
>4 5
settings6 >
,> ?
ILogger 
logger 
) 
: 
base "
(" #
logger# )
)) *
{ 	
	_settings 
= 
settings  
;  !
} 	
	protected 
override 
string !

ServiceUrl" ,
=>- /
	_settings 
. 
Value 
. 
	Endpoints %
.% &
FirstOrDefault& 4
(4 5
e5 6
=>7 9
e: ;
.; <
	Reference< E
==F H
	EndpointsI R
.R S

HackerNewsS ]
)] ^
.^ _
BaseUrl_ f
;f g
} 
} ‰
tC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\Services\HackerNews\HackerNewsService.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %
Services% -
.- .

HackerNews. 8
{ 
public 

class 
HackerNewsService "
:# $
Service% ,
<, -
IHackerNewsClient- >
>> ?
,? @
IHackerNewsServiceA S
{ 
private 
readonly 
IOptions !
<! "
AppSettings" -
>- .
	_settings/ 8
;8 9
public 
HackerNewsService  
(  !
IHackerNewsClient! 2
hackerNewsClient3 C
,C D
IOptions 
< 
AppSettings  
>  !
settings" *
)* +
:, -
base. 2
(2 3
hackerNewsClient3 C
)C D
{ 	
	_settings 
= 
settings  
;  !
} 	
public 
List 
< 
long 
> %
GetListOfBestHistoriesIds 3
(3 4
)4 5
{ 	
var 
method 
= 
	_settings "
." #
Value# (
.( )
	Endpoints) 2
.2 3
FirstOrDefault3 A
(A B
eB C
=>D F
eG H
.H I
	ReferenceI R
==S U
	EndpointsV _
._ `

HackerNews` j
)j k
.k l
Methodsl s
[s t
$numt u
]u v
;v w
return 
Client 
. 
Get 
< 
List "
<" #
long# '
>' (
>( )
() *
method* 0
)0 1
.1 2

GetAwaiter2 <
(< =
)= >
.> ?
	GetResult? H
(H I
)I J
;J K
} 	
public 
New 
GetNewDetails  
(  !
long! %
newId& +
)+ ,
{   	
var!! 
method!! 
=!! 
	_settings!! "
.!!" #
Value!!# (
.!!( )
	Endpoints!!) 2
.!!2 3
FirstOrDefault!!3 A
(!!A B
e!!B C
=>!!D F
e!!G H
.!!H I
	Reference!!I R
==!!S U
	Endpoints!!V _
.!!_ `

HackerNews!!` j
)!!j k
.!!k l
Methods!!l s
[!!s t
$num!!t u
]!!u v
;!!v w
var"" 
id"" 
="" 
newId"" 
."" 
ToString"" #
(""# $
)""$ %
;""% &
return$$ 
Client$$ 
.$$ 
Get$$ 
<$$ 
New$$ !
>$$! "
($$" #
method$$# )
.$$) *
Replace$$* 1
($$1 2
$str$$2 8
,$$8 9
id$$: <
)$$< =
)$$= >
.$$> ?

GetAwaiter$$? I
($$I J
)$$J K
.$$K L
	GetResult$$L U
($$U V
)$$V W
;$$W X
}%% 	
}&& 
}'' »
jC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Infraestructure\Tools\SafeCaller\SafeCaller.cs
	namespace 	

HackerNews
 
. 
Infraestructure $
.$ %
Tools% *
.* +

SafeCaller+ 5
{ 
public		 

static		 
class		 

SafeCaller		 "
{

 
public 
static 
void 
SafeCall #
(# $
Action$ *
action+ 1
,1 2
ILogger3 :
logger; A
)A B
{ 	
var 
retryPolicy 
= 
Policy $
.$ %
Handle% +
<+ ,
	Exception, 5
>5 6
(6 7
)7 8
.8 9
Retry9 >
(> ?
$num? @
,@ A
(B C
eC D
,D E

retryCountF P
)P Q
=>R T
{ 
logger 
. 
Error 
( 
$" 
Retry  %
{% &

retryCount& 0
}0 1
 returned error 1 A
{A B
eB C
}C D
"D E
)E F
;F G
} 
) 
; 
retryPolicy 
. 
Execute 
(  
(  !
)! "
=># %
{ 
action 
( 
) 
; 
} 
) 
; 
} 	
} 
} 