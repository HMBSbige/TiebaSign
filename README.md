# 百度贴吧自动签到
需要知道 `BDUSS`（登录百度，随便在 cookie 中就能找到了）

## Docker
```
docker run -it --restart=always --name=tieba hmbsbige/tiebasign $BDUSS
```
