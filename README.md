# trans4d-csharp

## Overview

trans4d-csharp is a modern, object-oriented C# port and refactor of the legacy TRANS4D/HTDP geodetic transformation software originally developed by the US National Geodetic Survey. It enables precise transformation of geodetic coordinates (latitude, longitude, height) between reference frames (datums) and epochs, including time-dependent effects like crustal motion. This is essential for converting GNSS (GPS) positions (typically referenced in ITRF at the date of collection) into North American datums like NAD83(2011).

## How to Use

The main entry point for transformations is `Trans4d.TransformPosition`. For example, to convert a GNSS coordinate collected on June 1, 2025 (ITRF2014, epoch 2025-06-01) to NAD83(2011) (epoch 2010-01-01):
var source = new GeodeticCoordinates(/* latitude, longitude, height */);
var sourceDatumEpoch = new DatumEpoch(Datum.ITRF2014, new DateTime(2025, 6, 1));
var targetDatumEpoch = new DatumEpoch(Datum.Nad83_2011_or_CORS96, new DateTime(2010, 1, 1));
var result = Trans4d.TransformPosition(source, sourceDatumEpoch, targetDatumEpoch);


## Main Components & Data Structures

- `Trans4d.TransformPosition`: Main method for transforming coordinates between datums/epochs.
- `Ellipsoid`, `CoordinateTransformer`: Convert between geodetic and cartesian coordinates.
- `DatumEpoch`: Represents a datum at a specific epoch.
- `XYZ`: Cartesian coordinates.
- `VelocityInfo`: Velocity components for time-dependent transformations.
- `GeodeticCoordinates`: Latitude, longitude, height.
- `Datum` (enum): Supported datums for transformation.

Velocity models are implemented using polymorphic classes for grid-based and plate motion approaches.

## Future Work & Enhancements

- Lots of code cleanup and refactoring.
- Add more test coordinates from all regions and validate against the Fortran version.
- Implement earthquake and post-seismic displacement modeling.
- Support for new NSRS 2025 terrestrial reference frames (NATREF, CATREF, MATREF, PATREF).
- Add geodetic utilities (Vincenty Direct/Inverse for geodesic lengths).
- Orthometric height calculations using geoid models (may be a separate repo).

## Feedback & Support

- Open a project issue on GitHub.
- Email: collin@sonicscholar.com
- Support SonicScholar: [PayPal link](https://www.paypal.com/ncp/payment/7XJN5H2M6TZJL)

## Behind the Scenes

This project began as a line-by-line port of Dr. Richard Snay's TRANS4D Fortran program into C++, then evolved into a C# refactor for modularity, testability, and future portability. The object-oriented approach makes the code easier to read, maintain, and port to other languages, and enables AI-assisted refactoring. Many classes are simple data structures to encapsulate related properties, improving clarity over legacy code.
TRANS4D is essentially a derivative of HTDP (Horizontal Time-Dependent Positioning), originally developed by the National Geodetic Survey (NGS), a division of the National Oceanic and Atmospheric Administration (NOAA) of the United States. This project builds on the foundational work and algorithms provided by those organizations.