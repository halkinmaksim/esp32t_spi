#   Halkin Maksim

import time
import machine

print("ESP32 Temperature sensor MAX6675")

spi = machine.SPI(spihost=1,sck=18, miso=19,mosi=21, cs=5,duplex=False)


i=0
try:


except KeyboardInterrupt as e:
    print("main.py except KeyboardInterrupt")
    
except Exception:
    print("main.py except Exception")
    machine.reset()


    
while True:
a=spi.read(2)
print((((a[0]<<8)|(a[1]))>>3)*0.25)
time.sleep(1)
