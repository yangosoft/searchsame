# Introduction #

SearchSame es una utilidad para buscar archivos duplicados en directorios.

# Instalación #
## Windows ##
Descargar [Microsoft .NET Framework 3.5](http://www.microsoft.com/downloads/details.aspx?FamilyID=333325FD-AE52-4E35-B531-508D977D32A6) y colocar el ejecutable de searchsame en cualquier directorio.

Ejecutar la consola de Windows _Start -> Execute -> cmd.exe_ y luego ejecutar el programa en el directorio donde se encuentre.

## Linux ##
Descargar Mono _sudo aptitude install mono_ y ejecutar la aplicación con $mono searchsame.exe

# Opciones de la línea de comandos #

searchsame.exe [-e] directorios

  * directorios: Se refiere a la ruta de los directorios donde se van a buscar los archivos. Por ejemplo "C:\windows" or "/usr/src"
  * Se pueden poner múltiples directorios: _searchsame.exe "C:\windows" "C:\dir2" "C:\dir3"_
  * -e poniendo este flag searchsame hará una comparación 1 a 1 de cada fichero mediante el hash md5 del contenido. Esta opción puede resultar muy lenta.

# Salida #

La salida del programa es
` OriginalFileName = DuplicatedFileName = md5Hash `

  * OriginalFileName: el primer fichero encontrado que se considera el original.
  * DuplicatedFileName: el fichero repetido.
  * md5Hash: hash md5 del contenido del fichero.
  * = es un token para separar cada campo.

Si el flag -e está presente se mostrará, además, una lista con el hash de cada fichero procesado tal que:
```
md5Hash FileName1
md5Hash FileName2
md5Hash FileName3
```