.model small
.data
	mesaj db "Introduceti numar de o cifra: \$"
	nr db ?
	
.code
start:
	mov ax, @data
	mov ds, ax
	mov dx, offset mesaj
	mov ah, 9
	int 21h
	
	mov ah, 01
	int 21h
	
	sub al, 30h
	mov ah, 00
	xor  bx, bx  ; suma se initializeaza cu 0
    mov  cx, ax  ; itereaza de la numarul citit la 1
iterate:
    mov  ax, cx
    mul  cx      ; calculeaza patratul numarului curent
    add  bx, ax  ; adauga rezultatul la suma finala
    loop iterate
	
	mov ah, 4ch
	int 21h
	
end start