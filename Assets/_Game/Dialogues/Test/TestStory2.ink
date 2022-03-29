-> main

=== main ===
Sample text 1 #name:Person 1 #fs:italic
Sample text 2 #name:Person 2 #fs:bold
Sample choice #name:Person 1 #fs:italic #fs:bold
    +[One] 
        -> C1
    +[Two] 
        -> C2
    +[Three] 
        -> C3
    
=== C1 ===
Chose 1 #name:Person 1
Choice 2 #name:Person 2
    +[Four] 
        -> C4
    +[Five] 
        -> C5

=== C2 ===
Chose 2 #name:Person 1
Choice 3 #name:Person 2
    +[Six] 
        -> C6
    +[Seven] 
        -> C7
    
=== C3 ===
Chose 3 #name:Person 1
-> END

=== C4 ===
Chose 4 #name:Person 1
-> END

=== C5 ===
Chose 5 #name:Person 1
-> END

=== C6 ===
Chose 6 #name:Person 1
-> END

=== C7 ===
Chose 7 #name:Person 1
-> END