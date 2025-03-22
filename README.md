# SvgBuilder
The repository for this project is publicly available at https://github.com/colinlsmatthews/SvgBuilder

**v.1.0.0** (released 25.03.22 at 5pm EDT) can be found on the `main` branch. There might be further developments on a different branch, but they will have been submitted after the release date.

## Instructions
To run or build the SVG Builder application directly from source, clone or download this repository and run `.\Console\Program.cs`

Alternatively, you can run this program as a targeted, self-contained executable from `.\Executables\{version}\{operating_system}`

## Description
**SvgBuilder v1.0.0** is a console application that accepts JSON input according to the following example:
```json
{
  "boxes": [
    {
      "min": [
        331,
        453
      ],
      "max": [
        354,
        463
      ],
      "color": "#ffff00"
    },
    //...additional boxes,
  ], //could be extended for aditional shapes:
// "circles": [
//   {
//       "center": [
//           300,
//           400
//       ],
//       "radius": 150
//   }
//  ]
}
```
The program parses the data contained within a given JSON file and builds an SVG file that depicts the shapes described by that data. Currently only rectangles are supported, but the architecture is set up with future extensibilty in mind.

The program outputs a `.svg` file to the directory specified. Users can input a custom width and height for the SVG or simply use the bounds of the specified shapes.

## Architecture
### SvgBuilder.CLI
**v1.0.0** is a pure console application with no UI. The program is initiated in `SvgBuilder.CLI.Program`, which handles user interactions. Methods in this class handle input validation (currently missing capability to check validity of input dimensions, which is instead handled by `SvgBuilder.Core.SvgSpec`). This class also instantiates the `Builder` class and runs `Build()` before prompting the user if they would like to build another SVG. Currently there is no option for a custom filename (output filename is just the same as whatever the input JSON is), but that should definitely be added in.

### SvgBuilder.Core

`Builder.cs` is intended to be a singleton class with one static method to build the SVG from gathered inputs (singleton pattern yet to be implemented in code). This class performs the following functions:
- Perform additional validation
- Construct destination path for SVG
- Read and Parse JSON data into JObject format
- Construct a `SvgSpec` object that contains parsed data
- Build XML text in SVG format
- Write text to disk

`SvgSpec.cs` provides a container object for parsed JSON data. Currently the only functionality built into this class is a validation check for input dimensions for the SVG file, and a small calculation for the centering transformation. This logic is contained within the class constructors, which is probably bad practice. Future versions should correct this.

`SvgElement.cs` is a simple struct that represents each shape from the parsed JSON file. It currently only supports rectangles and should be extended to encompass other SVG shapes. One way to do this would be to create child classes that inherit from `SvgElement` (such as `SvgRectangle : SvgElement`) or perhaps instead use an interface.

`Util.cs` is a simple static utility class with no accessible constructor. It contains logic for validation, string-building, and calculating the overall bounds of the input elements.

### SvgBuilder.UI
This project just contains boilerplate files for a standard WPF app and is intended to handle user interaction with SvgBuilder through a windowed user interface. It hasn't been developed yet.

## Dependencies
Project **`SvgBuilder.CLI`** references project `SvgBuilder.Core`, uses the `Microsoft.NETCore.App` framework, and has no external dependencies.

Project **`SvgBuilder.Core`** has no references, uses the `Microsoft.NETCore.App` framework, and is dependent on the `Newtonsoft.Json` package, available from NuGet.

Project **`SvgBuilder.UI`** references `SvgBuilder.Core`, uses the `Microsoft.NETCore.App` and `Microsoft.WindowsDesktop.App.WPF` frameworks, and has no external dependencies.

## Future improvements:
- Allow for dimension validation check during input process
- Allow for custom filename
- Implement singleton pattern for builder
- Extend builder to handle additional types of shapes
- Move validation logic for `SvgSpec` out of constructors
- Implement user interface via `SvgBuilder.UI` using WPF and MVVM architecture
- General refactoring and cleanup
