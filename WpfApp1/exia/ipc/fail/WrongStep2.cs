using WpfApp1.exia.ipc.entities;

namespace WpfApp1.exia.ipc.fail;

public class WrongStep2 : IStep2Strategy 
{
    public WrongStep2(){}
    
    public void onMachineRequest(MachineX applicant, MachineY executor){}

    public void onMachineExecute(MachineX applicant, MachineY executor)
    {
        executor.executeJob();
    }
}