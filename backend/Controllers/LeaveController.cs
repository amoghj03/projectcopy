using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Models.DTOs;
using BankAPI.Services;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveController : ControllerBase
{
    private readonly ILeaveService _leaveService;
    private readonly ApplicationDbContext _context;

    public LeaveController(ILeaveService leaveService, ApplicationDbContext context)
    {
        _leaveService = leaveService;
        _context = context;
    }

    /// <summary>
    /// Submit a new leave request
    /// </summary>
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitLeaveRequest([FromBody] SubmitLeaveRequest request)
    {
        try
        {
            // Get employee ID from authorization (simplified - in production use JWT claims)
            var employeeId = GetEmployeeIdFromAuth();
            if (employeeId == 0)
            {
                return Unauthorized(new { success = false, message = "Unauthorized access." });
            }

            var result = await _leaveService.SubmitLeaveRequestAsync(employeeId, request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"An error occurred: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Get all leave requests for the authenticated employee
    /// </summary>
    [HttpGet("requests")]
    public async Task<IActionResult> GetLeaveRequests()
    {
        try
        {
            var employeeId = GetEmployeeIdFromAuth();
            if (employeeId == 0)
            {
                return Unauthorized(new { success = false, message = "Unauthorized access." });
            }

            var result = await _leaveService.GetLeaveRequestsAsync(employeeId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"An error occurred: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Get leave balance for the authenticated employee
    /// </summary>
    [HttpGet("balance")]
    public async Task<IActionResult> GetLeaveBalance()
    {
        try
        {
            var employeeId = GetEmployeeIdFromAuth();
            if (employeeId == 0)
            {
                return Unauthorized(new { success = false, message = "Unauthorized access." });
            }

            var result = await _leaveService.GetLeaveBalanceAsync(employeeId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"An error occurred: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Get a specific leave request by ID
    /// </summary>
    [HttpGet("requests/{id}")]
    public async Task<IActionResult> GetLeaveRequestById(long id)
    {
        try
        {
            var employeeId = GetEmployeeIdFromAuth();
            if (employeeId == 0)
            {
                return Unauthorized(new { success = false, message = "Unauthorized access." });
            }

            var result = await _leaveService.GetLeaveRequestByIdAsync(employeeId, id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"An error occurred: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Get available leave types for the tenant
    /// </summary>
    [HttpGet("types")]
    public async Task<IActionResult> GetLeaveTypes()
    {
        try
        {
            var employeeId = GetEmployeeIdFromAuth();
            if (employeeId == 0)
            {
                return Unauthorized(new { success = false, message = "Unauthorized access." });
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return NotFound(new { success = false, message = "Employee not found." });
            }

            var leaveTypes = await _context.LeaveTypes
                .Where(lt => lt.TenantId == employee.TenantId && lt.IsActive)
                .Select(lt => new
                {
                    id = lt.Id,
                    name = lt.Name,
                    description = lt.Description,
                    maxDaysPerYear = lt.MaxDaysPerYear,
                    isPaid = lt.IsPaid
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Leave types retrieved successfully.",
                data = leaveTypes
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"An error occurred: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// Helper method to extract employee ID from authorization token
    /// In production, this should decode JWT token and extract employee ID from claims
    /// </summary>
    private long GetEmployeeIdFromAuth()
    {
        try
        {
            // Get token from Authorization header
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return 0;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Decode token (simplified - in production use proper JWT validation)
            var tokenBytes = Convert.FromBase64String(token);
            var tokenData = System.Text.Encoding.UTF8.GetString(tokenBytes);
            var parts = tokenData.Split(':');

            if (parts.Length < 2)
            {
                return 0;
            }

            var userId = long.Parse(parts[0]);

            // Get employee ID from user ID
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == userId);
            return employee?.Id ?? 0;
        }
        catch
        {
            return 0;
        }
    }
}
