namespace WpfApp1.exia.ipc.entities;

public interface IStep2Strategy
{
    void onMachineRequest(MachineX var1, MachineY var2);

    void onMachineExecute(MachineX var1, MachineY var2);
}