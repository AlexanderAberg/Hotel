SELECT Guests.GuestId, Guests.LastName, Bookings.IsPaid, Bookings.PaymentDueDate FROM Guests
INNER JOIN Bookings ON Guests.GuestId = Bookings.GuestId


SELECT Rooms.RoomNumber, Bookings.CheckIn, Bookings.NumberOfGuests FROM Rooms
INNER JOIN Bookings ON Bookings.NumberOfGuests = Rooms.RoomNumber

SELECT COUNT(GuestId), City
FROM Guests
GROUP BY City;

SELECT *
FROM Bookings
WHERE BookingId IN (
    SELECT BookingId
    FROM Bookings
    WHERE IsPaid = 0
    AND PaymentDueDate < CAST(GETDATE() AS DATE)
)
