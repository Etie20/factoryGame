using WpfApp1.exia.ipc.entities;

namespace WpfApp1.exia.ipc.fail;

public class WrongStep1: IStep1Strategy
{
    public WrongStep1(){}

    public Product onMachineRequest(InputDock dock, MachineX machine)
    {
        return dock.accept();
    }
}