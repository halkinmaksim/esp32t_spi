#   Halkin Maksim


#
#
#
#   ampy --port COM13 put MAX6675.py
#
#

"""
import MAX6675
sensors = MAX6675.MAX6675()
sensors.getAllTemperature()

sensors.dict_cs_pins
sensors.dict_cs_obj
sensors.getTemperature(0)

sensors.getAllTemperature()

ampy --port COM13 put MAX6675.py
ampy --port COM13 put main.py


"""

import time
import machine
from machine import Pin as GPIO
import MAX6675

print("ESP32 Temperature sensor MAX6675")




sensors = MAX6675.MAX6675()
sensors.StartThrReadTemperature()
i=0
try:
    while True:
        #sensors.getAllTemperature()
        sensors.printTemperature()
        time.sleep(1)
        i=i+1

except KeyboardInterrupt as e:

    print("main.py except KeyboardInterrupt")
    sensors.StopThrReadTemperature()
    #machine.reset()
except Exception:
    print("main.py except Exception")
    sensors.StopThrReadTemperature()
    machine.reset()

