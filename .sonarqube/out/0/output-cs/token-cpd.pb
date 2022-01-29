ﬁ
YC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Constants\Endpoints.cs
	namespace 	

HackerNews
 
. 
Domain 
. 
	Constants %
{ 
public 

static 
class 
	Endpoints !
{ 
public		 
const		 
string		 

HackerNews		 &
=		' (
$str		) 5
;		5 6
}

 
} ﬂ
hC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Entities\Attributes\RedisAttribute.cs
	namespace 	

HackerNews
 
. 
Domain 
. 
Entities $
.$ %

Attributes% /
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public 

class 
RedisAttribute 
:  !
	Attribute" +
{		 
public

 
string

 
	Reference

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
} 
} í
_C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Entities\Base\AppSettings.cs
	namespace 	

HackerNews
 
. 
Domain 
. 
Entities $
.$ %
Base% )
{ 
public 

class 
AppSettings 
{ 
public		 
List		 
<		 
Endpoint		 
>		 
	Endpoints		 '
{		( )
get		* -
;		- .
set		/ 2
;		2 3
}		4 5
public

 
List

 
<

 
RedisConfigs

  
>

  !
RedisConfigs

" .
{

/ 0
get

1 4
;

4 5
set

6 9
;

9 :
}

; <
} 
public 

class 
RedisConfigs 
{ 
public 
string 
	Reference 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
ConnectionString &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
} 
public 

class 
Endpoint 
{ 
public 
string 
	Reference 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
BaseUrl 
{ 
get  #
;# $
set% (
;( )
}* +
public 
List 
< 
string 
> 
Methods #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} Ö
]C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Entities\HackerNews\New.cs
	namespace 	

HackerNews
 
. 
Domain 
. 
Entities $
.$ %

HackerNews% /
{ 
public		 

class		 
New		 
{

 
public 
string 
By 
{ 
get 
; 
set  #
;# $
}% &
public 
long 
Descendants 
{  !
get" %
;% &
set' *
;* +
}, -
public 
long 
Id 
{ 
get 
; 
set !
;! "
}# $
public 
List 
< 
long 
> 
Kids 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
long 
Score 
{ 
get 
;  
set! $
;$ %
}& '
public 
long 
Time 
{ 
get 
; 
set  #
;# $
}% &
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Type 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Url 
{ 
get 
;  
set! $
;$ %
}& '
} 
} ˙
cC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Entities\Integration\Response.cs
	namespace 	

HackerNews
 
. 
Domain 
. 
Entities $
.$ %
Integration% 0
{ 
public 

class 
Response 
{		 
public

 
Response

 
(

 
)

 
{ 	
} 	
public 
Response 
( 
object 
content &
)& '
{ 	
Content 
= 
content 
; 
} 	
public 
Response 
( 
string 
error $
)$ %
{ 	
Error 
= 
error 
; 
} 	
public 
object 
Content 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Error 
{ 
get !
;! "
set# &
;& '
}( )
public 
Task 
< 
Response 
> 
GetResponseAsTask /
(/ 0
)0 1
{ 	
return 
Task 
. 

FromResult "
(" #
this# '
)' (
;( )
} 	
} 
}   ≠
wC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\DataAccess\Redis\Base\IRedisBase.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -

DataAccess- 7
.7 8
Redis8 =
.= >
Base> B
{ 
public 

	interface 

IRedisBase 
<  
T  !
>! "
{ 
void		 
Add		 
(		 
string		 
key		 
,		 
object		 #
value		$ )
,		) *
TimeSpan		+ 3
?		3 4
	expiresIn		5 >
=		? @
null		A E
)		E F
;		F G
T

 	
Get


 
(

 
string

 
key

 
)

 
;

 
} 
} °
áC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\DataAccess\Redis\Factory\IRedisConnectionFactory.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -

DataAccess- 7
.7 8
Redis8 =
.= >
Factory> E
{ 
public 

	interface #
IRedisConnectionFactory ,
{		 
public

 !
ConnectionMultiplexer

 $#
GetHackerNewsConnection

% <
(

< =
)

= >
;

> ?
} 
} „
xC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\DataAccess\Redis\IHackerNewsRedis.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -

DataAccess- 7
.7 8
Redis8 =
{ 
public		 

	interface		 
IHackerNewsRedis		 %
:		& '

IRedisBase		( 2
<		2 3
List		3 7
<		7 8
New		8 ;
>		; <
>		< =
{

 
} 
} à
eC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\Logger\ILogger.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -
Logger- 3
{ 
public 

	interface 
ILogger 
{ 
void		 
Error		 
(		 
string		 
message		 !
)		! "
;		" #
void

 
Info

 
(

 
string

 
message

  
)

  !
;

! "
void 
Warning 
( 
string 
message #
)# $
;$ %
void 
Debug 
( 
string 
message !
)! "
;" #
} 
} ı
pC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\Services\Base\IRESTClient.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -
Services- 5
.5 6
Base6 :
{ 
public 

	interface 
IRESTClient  
{		 
Task

 
<

 
TResult

 
>

 
Get

 
<

 
TResult

 !
>

! "
(

" #
string

# )
method

* 0
)

0 1
where 
TResult 
: 
class !
;! "
} 
} «
mC:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\Services\Base\IService.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -
Services- 5
.5 6
Base6 :
{ 
public 

	interface 
IService 
{ 
}		 
}

 ç
|C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\Services\HackerNews\IHackerNewsClient.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -
Services- 5
.5 6

HackerNews6 @
{ 
public 

	interface 
IHackerNewsClient &
:' (
IRESTClient) 4
{		 
}

 
} ˚
}C:\Users\Guilherme Rocha\source\repos\HackerNews\HackerNews.Domain\Interfaces\Infra\Services\HackerNews\IHackerNewsService.cs
	namespace 	

HackerNews
 
. 
Domain 
. 

Interfaces &
.& '
Infra' ,
., -
Services- 5
.5 6

HackerNews6 @
{ 
public 

	interface 
IHackerNewsService '
{		 
List

 
<

 
long

 
>

 %
GetListOfBestHistoriesIds

 ,
(

, -
)

- .
;

. /
New 
GetNewDetails 
( 
long 
newId $
)$ %
;% &
} 
} 