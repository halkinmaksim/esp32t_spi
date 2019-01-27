# esp32t_spi
ESP32 with temperature sensore MAX6675

esptool.py --chip esp32 --port COM8 --before default_reset --after no_reset write_flash -z --flash_mode dio --flash_freq 40m --flash_size detect 0x1000 bootloader.bin 0xf000 phy_init_data.bin 0x10000 MicroPython.bin 0x8000 partitions_mpy.bin 


ampy --port COM8 put MAX6675.py
ampy --port COM8 put main.py