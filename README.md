# AnagramAssignment

- Language: C#

- Manual Install: Click on latest release, download the self-contained executable
  - .exe for Windows
  - without extension for Mac OS
 
 Place the self-contained executable in a globally accessible folder for your shell (e.g /bin or C:\bin) and make sure this folder is registered in your PATH environment variable.
 
 Run command: 
 
 ```
 anagramassignment path_to_file
 ```

- Big O:
  - File with 1.3m lines 
  - ![FileSize](https://user-images.githubusercontent.com/64783856/137738160-c96ba2ca-d5e9-44af-b2df-dbcffb8fd2cc.PNG)
  - Space complexity - ![SpaceComplexity](https://user-images.githubusercontent.com/64783856/137738269-e980630c-bb9b-4844-9c22-266ab8293fd7.PNG)
  - Time complexity - O(n\*m) where O(n^2) is the worst case 
  
          - loops the file once
          - loops each item of file once using LINQ .OrderBy() method which implements quicksort
          - loops the grouped items once when outputting
        
 - Improvements and thoughts: 
   - Parsing the file:
     - Using a string to store current line: https://cc.davelozinski.com/c-sharp/fastest-way-to-read-text-files
     - Might be a more optimal solution using buffers even though it doesn't seem that way from the benchmarks 
     - Would need to test multiple implementations to decide
     - Think about using a more optimized algorithm for grouping anagrams
     - This implementation should perform well unless the items themselves are huge. Still retains the benefit of readability and minimal code
   - Should explore behaviour of File.OpenText() and ReadLineAsync() and update exception handling accordingly, right now we have a catch all block
