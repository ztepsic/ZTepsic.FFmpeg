:: about ffprobe --> http://ffmpeg.org/ffprobe.html
:: about librtmp --> http://rtmpdump.mplayerhq.hu/librtmp.3.html

:: rtmp --> WORKING streams
set out_file_prefix=rtmp

:: #####################
set resource="rtmpt://188.165.205.104:443/live/demo live=1"
set out_file_sufix=01_not

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################

:: #####################
set resource="rtmp://85.25.122.231/streamHD/video/stream live=1"
set out_file_sufix=02

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################

:: #####################
set resource="rtmp://cp99495.live.edgefcs.net/live/Flash_live_KTO_TV@27823 live=1"
set out_file_sufix=03

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################

:: mmst MMS (Microsoft Media Server) protocol over TCP --> WORKING streams
set out_file_prefix=mmst

:: #####################
set resource=mmst://203.146.129.248/dara
set out_file_sufix=01

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################

:: mmsh MMS (Microsoft Media Server) protocol over HTTP. --> WORKING streams
set out_file_prefix=mmsh

:: #####################
set resource=mmsh://rts.videostreaming.rs/rts
set out_file_sufix=01

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################

:: #####################
set resource=mmsh://sepidehlive.nanoservers.net/sepideh
set out_file_sufix=02_not

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################

:: flv --> WORKING streams
set out_file_prefix=flv
:: #####################
set resource=http://flash.live.tv-radio.com/mce/all/mce-h264.flv
set out_file_sufix=01

set print_format=default
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

set print_format=xml
ffprobe.exe -i %resource% -print_format %print_format% -show_error -show_format -show_streams > %out_file_prefix%_%out_file_sufix%.%print_format%

:: #####################