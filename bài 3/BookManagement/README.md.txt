GIẢI THÍCH LUỒNG HOẠT ĐỘNG CỦA CODE – BÀI 6 VALIDATION CƠ BẢN
1. Mục đích bài tập: Xây dựng chức năng thêm sách trong ASP.NET MVC và kiểm tra dữ liệu đầu vào trước khi lưu.
Yêu cầu: Tên sách không được để trống; Giá sách phải lớn hơn 0; sử dụng Data Annotation và ModelState.

2. Luồng hoạt động của chương trình
Bước 1: Người dùng truy cập trang Add Book. Controller trả về View chứa form nhập Tên sách và Giá sách.
Bước 2: Người dùng nhập dữ liệu và nhấn Submit. Dữ liệu được gửi đến phương thức POST Add(Book book).
Bước 3: Data Annotation kiểm tra dữ liệu. [Required] kiểm tra tên sách không rỗng, [Range] kiểm tra giá lớn hơn 0.
Bước 4: Controller kiểm tra ModelState.IsValid. Nếu dữ liệu không hợp lệ, form được hiển thị lại và thông báo lỗi xuất hiện.
Bước 5: Nếu dữ liệu hợp lệ, sách được thêm vào danh sách và hệ thống chuyển về trang danh sách sách.

3. Vai trò của Data Annotation
Data Annotation giúp khai báo các quy tắc kiểm tra dữ liệu trực tiếp trong Model, giảm mã xử lý trong Controller và dễ bảo trì.

4. Vai trò của ModelState
ModelState lưu kết quả kiểm tra dữ liệu. ModelState.IsValid trả về TRUE khi dữ liệu hợp lệ và FALSE khi có lỗi.

5. Kết luận
Bài tập sử dụng Data Annotation để xác định các điều kiện hợp lệ và ModelState để kiểm tra dữ liệu trước khi lưu. Khi dữ liệu sai, hệ thống hiển thị lỗi; khi dữ liệu đúng, sách được thêm thành công.
