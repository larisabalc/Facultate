.model small
.stack 100H
.data
array db 55, 32, 98, 21, 13, 12, 38, 25, 56, 16
smallest db ?
.code
main proc
    mov ax, @data
    mov ds, ax
    mov bx, 0
    mov al, array[bx]	
    mov smallest, al	
    
compare:
    inc bx 
    cmp bx, 10 
    je exit
    mov al, array[bx]
    cmp al, smallest	
    jl update_smallest
    jmp compare
	
update_smallest:
    mov smallest, al
    jmp compare
	
exit:
	mov al, smallest     
    aam                
	add ax, 3030h      
	push ax            
	mov dl, ah        
	mov ah, 02h        
	int 21h
	pop dx             
	mov ah, 02h        
	int 21h
	mov ah, 4ch
	int 21h
    main endp
end main


