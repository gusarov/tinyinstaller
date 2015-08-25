# Principle #

MSI uses special table and `[Bracket]` syntax for variables expanding. Environment variables is also available with this syntax. MSI also has special properties for actions that allow persist variable as environment variable for better interoperability. I've decided do not invent a bicycle and just use system environment variables everywhere. You can execute Environment.Expand in .NET application, or just use it from batch files.

# List #

  * TargetDir