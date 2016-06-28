![icon](http://www.gridprotectionalliance.org/images/products/icons%2064/streamsplitter.png)![Logo](https://raw.githubusercontent.com/GridProtectionAlliance/phasorsplitter/master/readme%20files/Logo.png)

# Overview

The Synchrophasor Stream Splitter is used to generate multiple data streams from a single synchrophasor stream source. This service based application consumes an incoming stream of synchrophasor data (e.g., from a substation with limited bandwidth where sending multiple streams would be impractical) and redistributes the stream as many times as needed.

All common synchrophasor protocols are supported (e.g., IEEE C37.118, IEC 61850-90-5, F-NET, SEL Fast Message, Macrodyne and BPA PDCstream). This tool does not disaggregate streams into points - it resends exactly what was received. Manager and console applications can be run remotely - active configuration can be downloaded, edited and uploaded from management tool. Configuration is stored as an XML file. Full support for incoming and outgoing UDP, TCP, IPv6, and IPv4 in all combinations.

For TCP proxy point, configuration frames are cached and sent to clients upon request. For UDP rebroadcasts, configuration frames will automatically be sent once per minute unless source connection is already doing this.

# Documentation and Support

* Get in contact with our development team on our new [discussion board](http://discussions.gridprotectionalliance.org/c/gpa-products/stream-splitter).
* Check out the [wiki](https://gridprotectionalliance.org/wiki/doku.php?id=sssplitter:overview).

# Deployment

To deploy:
1. Make sure your system meets all the [requirements](#requirements) below.
* Choose a [download](#downloads) option below.
* Unzip if necessary.
* Run "StreamSplitterSetup.msi".
* Follow the wizard.
* Enjoy.

## Requirements
* Windows 7 or newer.
* .NET 4.5 or newer.

## Downloads

* Download the latest stable release [here](https://github.com/GridProtectionAlliance/phasorsplitter/releases).
* Download old releases [here](http://phasorsplitter.codeplex.com/releases/view/112147).
* Download the nightly build [here](http://www.gridprotectionalliance.org/nightlybuilds/SynchrophasorStreamSplitter/Beta/) - click on Setup.zip.

# Contributing

If you would like to contribute to the Synchrophasor Stream Splitter project please:
* Read our [styleguide](https://www.gridprotectionalliance.org/docs/GPA_Coding_Guidelines_2011_03.pdf)
* Fork the repository.
* Do what you do best.
* Create a pull request.

# License
The Synchrophasor Stream Splitter is licensed under the [MIT license](https://opensource.org/licenses/MIT).
