#!/bin/bash

if [ -f .paket/paket.exe ]; then
    mono .paket/paket.bootstrapper.exe
fi

if [ -f boot.fsx ]; then
	fsharpi boot.fsx
	rm boot.fsx
	mono .paket/paket.exe install
fi

if [ ! -f paket.lock ]; then
	mono .paket/paket.exe install
fi

mono packages/build/FAKE/tools/FAKE.exe "build.fsx" Dummy --fsiargs build.fsx $@
