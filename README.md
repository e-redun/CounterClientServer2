﻿# CounterClientServer2
 Client(WPF C#)-Server(Console C#)(RUS comments). Socket, TcpListener. Клиент-Сервер 

Описание задачи:
Разработать 2 приложения - клиент и сервер.
Сервер должен быть выполнен в виде консольного приложения.
В настройках сервера должна быть возможность задать порт, на котором запускается приложение.
В настройках клиента должна быть возможность задать адрес сервера - ip и порт.
При старте клиента должно запускаться окно, на котором:
- Кнопка СТАРТ
- Кнопка СТОП
- СБРОС
- Поле для отображения значения счётчика При нажатии кнопки СТАРТ, на сервере должен запускаться счётчик. Период изменения значения счётчика - 1 секунда.
Значение счётчика должно отображаться на всех запущенных клиентах.
При нажатии кнопки СТОП на любом клиенте, счётчик должен останавливаться.
Кнопка СТОП на всех клиентах должна переименовываться в ПРОДОЛЖИТЬ.
При нажатии ПРОДОЛЖИТЬ счётчик начинает увеличиваться. Кнопка ПРОДОЛЖИТЬ на всех клиентах должна переименовываться в СТОП.
При нажатии СБРОС счётчик должен обнуляться. Должно отрабатывать в любой момент. И в момент остановки счётчика, и в процессе его увеличения.
Используемые технологии:
Язык C#
Net framework 4.6
Визуальная часть - WPF
Взаимодействие между клиентом и сервером должно быть реализовано двумя
способами:
- socket
- websocket (не сделано)
При взаимодействии через websocket данные должны передаваться в формате json.
Способ взаимодействия должен задаваться в настройках клиента.