# ClickBoard
Программа для автовставки в буфер обмена (или в поле ввода - по alt-tab) заранее сохраненного текста при клике по тексту в программе.

1.	Назначение программы: автоматизация операционной деятельности пользователя в рамках ввода типовых текстовых данных. 
Предполагается автоматический ввод в необходимое поле другой программы или в буфер обмена операционной системы текста, 
путем соответствующего выбора в программе.
2.	Цель создания программы: снижение трудозатрат при работе типовыми текстами, общее повышение грамотности текстов, 
через вставку заведомо грамотного текста и снижение нагрузки, которое оказывает на пользователя монотонная повторяющаяся работа. 

Окно программы содержит область данных и область управления.
В области данных содержатся вкладки, внутри каждой вкладки в виде двумерного дерева содержатся объединенные в блоки тексты.
В области управления - элементы управления: кнопка записи(сохранения) профиля; выпадающий список - выбор профиля; 
кнопки: поверх всех окон, вкл/выкл звука, вкл/выкл автодействия; кнопки сохранения позиции и размера окна. 

Для удобства тексты  группируются в блоки. Например:
[-]Животные
 | |-Собака
 | |-Кошка
 | |-Мышь
 |
[-]Рыбы
 | |-Окунь
 | |-Щука
 |
[+]Птицы

Наборы блоков (деревья) могут группироваться вкладками. Одна вкладка - одно дерево с блоками/текстами.

Тексты можно размечать цветом.

Для каждого текста возможно задать автодействие (в разработке). Предполагается что-то наподобие скриптов автодействий (нажатие клавиш и т.д.).
Для текста можно задать отображаемый текст. При наведении на обображаемый текст напоминание подсвечивается. 
Это сделано чтобы удобно, в краткой форме, хранить большие блоки текста.

Для использования несколькими пользователями можно задать каждому профиль.
Профиль хранит набор вкладок, набор параметров окна(размер/координаты на экране). 
Каждая вкладка содержит помимо текста ещё и настройки кнопок: поверх всех окон, вкл/выкл звука, вкл/выкл автодействия.
При сохранении профиля он сохраняет каждую вкладку в свой файл.

Размер и позиция окна позволяет запоминать эти параметры и восстанавливать их при открытии профиля.