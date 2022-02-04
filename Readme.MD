# CityWatch

TODO

# Broker

`docker pull eclipse-mosquitto`

`docker run -it -p 1883:1883 -p 9001:9001 -v mosquitto.conf:/mosquitto/config/mosquitto.conf eclipse-mosquitto`

Nakon toga pokrenuti CLI od tog dockera i u CLI editirati mosquitto.conf:

`vi /mosquitto/config/mosquitto.conf`

Izmijeniti linije:
`allow_anonymous true`
`listener 1883`
`persistence true`

Za izlaz - <kbd>ESC</kbd> `:wq` <kbd>Enter</kbd>